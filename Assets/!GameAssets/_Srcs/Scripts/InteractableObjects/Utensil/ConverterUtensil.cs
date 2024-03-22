//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/29"
//----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem.Interfaces;
using UnderworldCafe.DataPersistenceSystem.GameDatas;

namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Class that responsible to manage converter utensils behavior
    /// </summary>
    public class ConverterUtensil : Utensil
    {
        #region Dependency Injection
        TimeManager _timeManagerRef;
        #endregion

        [Header("=======[Converter Utensil Properties]=======")]
        [SerializeField] private List<ConverterUtensilStatsData> _statsDataPerLevel;
        public IReadOnlyList<ConverterUtensilStatsData> StatsDataPerLevel => _statsDataPerLevel;

        private ConverterUtensilStatsData _currentStatsData;

        private bool _isProcessing;
        private bool _isFoodReady;
        public Ingredient ReadyToTakeFood {get; private set;}


        //Validating Data
        protected override void OnValidate()
        {
            base.OnValidate();

            if(_statsDataPerLevel.Count <= 0)
            {
                Debug.LogWarning("No stats data set in " + gameObject.name);
            }
        }
        

        protected override void Start()
        {
            _timeManagerRef = LevelManager.Instance.LevelTimeManagerRef;

            base.Start();
            _isProcessing = false;
            _isFoodReady = false;

            // Need additional check for if player has loading save or not
            _currentStatsData = StatsDataPerLevel[0];
        }

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

                        StartCoroutine(ProcessingFood(recipe.RecipeInformation.RecipeOutput));

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
            // yield return new WaitForSeconds(_currentStatsData.ConvertingTime);   

            float _startProcessingTime = _timeManagerRef.TimePassed;
            while(_timeManagerRef.TimePassed - _startProcessingTime < _currentStatsData.ConvertingTime)
            {
                yield return null;
            }
            
            ReadyToTakeFood = createdIngredient;
            _isProcessing = false;
            _isFoodReady = true;
        }

        private void ReturnNewFood(PlayerInventory playerInventory, Ingredient newFood)
        {
            playerInventory.AddInventory(newFood);
            _isFoodReady = false;
        }

        
    }
}
