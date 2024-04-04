//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;


using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem;

namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Class that responsible to manage generator utensils behavior
    /// </summary>
    public class GeneratorUtensil : Utensil
    {
        [Header("=======[Generator Utensil Properties]=======")]
        [SerializeField] private Ingredient _generatedIngredient;


        [Header("=======[Generator Utensil Types]=======")]
        
        [Tooltip("Normal: Will add this utensil generated ingredient. \n" + 
                 "Pure: Will delete every ingredient in player inventory and then add this utensil generated ingredient. \n" +
                 "Conversion: Will add this utensil generated ingredient and then convert all the ingredients in player inventory to a new ingredient. \n")]
        [SerializeField] private GeneratorUtensilType _generatorUtensilType;
        
        [Header("Conversion Generator Properties (Fill only when selecting Converting Generator)")]
        [SerializeField] private List<GeneratorUtensilStatsData> _statsDataPerLevel;
        private GeneratorUtensilStatsData _currentStatsData;

        public enum GeneratorUtensilType
        {
            NORMAL_GENERATOR = 0,
            PURE_GENERATOR,
            CONVERSION_GENERATOR
        }
        public IReadOnlyList<GeneratorUtensilStatsData> StatsDataPerLevel => _statsDataPerLevel;
        

        //Validating Data
        protected override void OnValidate()
        {
            base.OnValidate();

            if(_generatedIngredient == null)
            {
                Debug.LogWarning("No generated ingredient data have been set on " + gameObject.name);
            }

            if(_generatorUtensilType == GeneratorUtensilType.CONVERSION_GENERATOR && _statsDataPerLevel.Count <= 0)
            {
                Debug.LogWarning("No stats data have been set on " + gameObject.name);
            }
        }


        protected override void Awake()
        {
            if(_generatorUtensilType == GeneratorUtensilType.CONVERSION_GENERATOR)
            {
                // Need additional check for if player has loading save or not
                _currentStatsData = _statsDataPerLevel[0];
            }

            _utensilAnimator.SetTrigger("Idling");
        }


        public override void Interact()
        {
            switch(_generatorUtensilType)
            {
                case GeneratorUtensilType.PURE_GENERATOR:
                    _playerControllerRef.PlayerInventory.RemoveInventoryAll();
                    ReturnNewFood(_playerControllerRef.PlayerInventory, _generatedIngredient);
                    break;

                case GeneratorUtensilType.NORMAL_GENERATOR:
                    ReturnNewFood(_playerControllerRef.PlayerInventory, _generatedIngredient);
                    break;

                case GeneratorUtensilType.CONVERSION_GENERATOR:
                    ReturnNewFood(_playerControllerRef.PlayerInventory, _generatedIngredient);
                    TryProcessInput(_playerControllerRef.PlayerInventory);
                    break;

                default:
                    Debug.LogError("Unknown GeneratorUtensilType");
                    break;
            }

            _utensilAnimator.SetTrigger("Idling");
        }

        private void TryProcessInput(PlayerInventory playerInventory)
        { 
            foreach(Recipe recipe in _currentStatsData.RecipeList)
            {
                if(recipe.RecipeInformation.Requirements.Count == playerInventory.PlayerInventoryList.Count)
                {
                    //This behavior does care about the order of ingredients
                    // if(UtensilRecipeComparer.IsEqualWithSameOrder(playerInventory.PlayerInventoryList, recipe.RecipeInformation.Requirements))


                    //This behavior does not care about the order of ingredients
                    // if(ListComparer.IsEqualWithoutSameOrder(playerInventory.PlayerInventoryList, recipe.RecipeInformation.Requirements))
                    if(UtensilComparer.IsEqualWithSameOrder(playerInventory.PlayerInventoryList, recipe.RecipeInformation.Requirements, ingredient => ingredient.IngredientInformation.Id))
                    {
                        // Debug.Log("Generator Recipe founded");
                        playerInventory.RemoveInventoryAll();
                        ReturnNewFood(playerInventory, recipe.RecipeInformation.RecipeOutput);
                        return;
                    }
                }
            }

            //If there are no matched recipe
            // Debug.Log("Generator Recipe not founded");
            playerInventory.RemoveInventoryAll();
            ReturnNewFood(playerInventory, FailedFood);
            return;
        }

        private void ReturnNewFood(PlayerInventory playerInventory, Ingredient newFood)
        {
            _utensilAnimator.SetTrigger("Generating");
            _audioManagerRef.PlaySFX(_audioManagerRef.UtensilGeneratingSFX);

            playerInventory.AddInventory(newFood);
        }
    }
}
