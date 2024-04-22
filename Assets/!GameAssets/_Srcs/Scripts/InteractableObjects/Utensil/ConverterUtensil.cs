//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/29"
//----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem;
using UnityEditor.EditorTools;

namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Class that responsible to manage converter utensils behavior
    /// </summary>
    public class ConverterUtensil : Utensil
    {
        #region Dependencies
        private TimeManager _timeManagerRef;
        [SerializeField] private Timer _timerVisualRef;
        #endregion

        [Header("=======[Converter Utensil Properties]=======")]
        
        [Tooltip("The Id of the utensil in Level / Local")]
        [SerializeField] private string LocalId;
        [SerializeField] private List<ConverterUtensilStatsData> _statsDataPerLevel;
        private ConverterUtensilStatsData _currentStatsData;
        private bool _isProcessing;
        private bool _isFoodReady;
        private Coroutine _processingCoroutine;
        
        public Ingredient ReadyToTakeFood {get; private set;}
        public IReadOnlyList<ConverterUtensilStatsData> StatsDataPerLevel => _statsDataPerLevel;


        #region MonoBehaviour
        //Validating Data
        protected override void OnValidate()
        {
            base.OnValidate();

            if(_statsDataPerLevel.Count <= 0)
            {
                Debug.LogWarning("No stats data set in " + gameObject.name);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _isProcessing = false;
            _isFoodReady = false;

            // Need additional check for if player has loading save or not
            _currentStatsData = StatsDataPerLevel[0];

            _utensilAnimator.SetTrigger("Idling");

            _timerVisualRef.ToggleTimerVisual(false);
        }

        protected void Start()
        {
            _timeManagerRef = LevelManager.Instance.TimeManager;
        }

        #endregion
        

        public override void Interact()
        {
            if(_isProcessing)
            {
                Debug.LogWarning("Already Processing");
                return;
            }

            if(!_isFoodReady) 
            {
                Debug.LogWarning("Food Not Ready");
                _isProcessing = TryProcessInput(_playerControllerRef.PlayerInventory);
                return;
            }

            //reset the player inv first
            _playerControllerRef.PlayerInventory.RemoveInventoryAll();

            //give back the food 
            ReturnNewFood(_playerControllerRef.PlayerInventory, ReadyToTakeFood);

            _utensilAnimator.SetTrigger("Idling");
        }

        private bool TryProcessInput(PlayerInventory playerInventory)
        { 
            foreach(Recipe recipe in _currentStatsData.RecipeList)
            {
                if(recipe.RecipeInformation.Requirements.Count == playerInventory.PlayerInventoryList.Count)
                {
                    //This behavior does care about the order of ingredients
                    if(UtensilComparer.IsEqualWithSameOrder(playerInventory.PlayerInventoryList, recipe.RecipeInformation.Requirements, ingredient => ingredient.IngredientInformation.Id))
                    {
                        // Debug.Log("Converter Recipe founded");

                        playerInventory.RemoveInventoryAll();

                        _processingCoroutine = StartCoroutine(ProcessingFood(recipe.RecipeInformation.RecipeOutput));

                        return true;
                    }
                }
            }

            //If there are no matched recipe
            // Debug.Log("Converter Recipe not founded");
            playerInventory.RemoveInventoryAll();
            ReturnNewFood(playerInventory, FailedFood);
            return false;
        }

        private IEnumerator ProcessingFood(Ingredient createdIngredient)
        {   
            float startProcessingTime = _timeManagerRef.TimePassed;
            float timePassed = 0;
            _timerVisualRef.ToggleTimerVisual(true);

            do 
            {
                timePassed = _timeManagerRef.TimePassed - startProcessingTime;
                _timerVisualRef.UpdateTimerSlider(timePassed, _currentStatsData.ConvertingTime);
                _utensilAnimator.SetTrigger("Processing");
                _audioManagerRef.PlaySFX(_audioManagerRef.UtensilProcessingSFX);
                yield return null;
            } 
            while(timePassed < _currentStatsData.ConvertingTime);
            
            _timerVisualRef.ToggleTimerVisual(false);
            ReadyToTakeFood = createdIngredient;
            _isProcessing = false;
            _isFoodReady = true;
        }

        private void ReturnNewFood(PlayerInventory playerInventory, Ingredient newFood)
        {
            _utensilAnimator.SetTrigger("Generating");
            _audioManagerRef.PlaySFX(_audioManagerRef.UtensilGeneratingSFX);

            playerInventory.AddInventory(newFood);
            _isFoodReady = false;
        }
    }
}
