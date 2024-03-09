//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

using UtilityCollections;

using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem.Interfaces;
using UnderworldCafe.DataPersistenceSystem.GameDatas;

namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
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
        public enum GeneratorUtensilType
        {
            NORMAL_GENERATOR = 0,
            PURE_GENERATOR,
            CONVERSION_GENERATOR
        }
        

        [Header("Conversion Generator Properties (Fill only when selecting Converting Generator)")]
        [SerializeField] private List<GeneratorUtensilStatsData> StatsDataPerLevel;
        private GeneratorUtensilStatsData _currentStatsData;

        protected override void Start()
        {
            base.Start();
            
            if(_generatorUtensilType == GeneratorUtensilType.CONVERSION_GENERATOR)
            {
                // Need additional check for if player has loading save or not
                _currentStatsData = StatsDataPerLevel[0];
            }
        }

        public override void Interact()
        {
            switch(_generatorUtensilType)
            {
                case GeneratorUtensilType.PURE_GENERATOR:
                    _playerControllerRef.PlayerInventory.RemoveInventoryAll();
                    _playerControllerRef.PlayerInventory.AddInventory(_generatedIngredient);
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
        }

        private void TryProcessInput(PlayerInventory playerInventory)
        {
            int inputSize = playerInventory.PlayerInventoryList.Count;

            //Get all recipe with same size as inputtedIngredient
            List<Recipe> recipeWithSameSize = new List<Recipe>();
            foreach (Recipe recipe in _currentStatsData.RecipeList)
            {
                int recipeRequirementSize = recipe.RecipeInformation.Requirements.Count;
                if(recipeRequirementSize == inputSize)
                {
                    recipeWithSameSize.Add(recipe);
                }
            }

            //If there are no matched size recipe
            if(recipeWithSameSize.Count <= 0)
            {
                playerInventory.RemoveInventoryAll();
                ReturnNewFood(playerInventory, FailedFood);
                return;
            }

            
            foreach(Recipe recipe in recipeWithSameSize)
            {
                //This behavior does care about the order of ingredients
                if(ListComparer.IsEqualWithSameOrder(playerInventory.PlayerInventoryList, recipe.RecipeInformation.Requirements))
                {
                    playerInventory.RemoveInventoryAll();
                    ReturnNewFood(playerInventory, recipe.RecipeInformation.RecipeOutput);
                    return;
                }
            }

            //If there are no matched recipe
            // Debug.LogWarning("Recipe not founded");
            playerInventory.RemoveInventoryAll();
            ReturnNewFood(playerInventory, FailedFood);
            return;
        }

        private void ReturnNewFood(PlayerInventory playerInventory, Ingredient newFood)
        {
            playerInventory.AddInventory(newFood);
        }
    }
}
