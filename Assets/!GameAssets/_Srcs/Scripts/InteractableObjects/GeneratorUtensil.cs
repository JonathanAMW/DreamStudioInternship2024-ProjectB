//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

using UtilityCollections;

using UnderworldCafe.Player;


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

        [Header("Pure Generator (Hover below field for explanation)")]
        [Tooltip("Only generate one ingredient / If has more, will delete PlayerInventory (CHOOSE ONE ONLY!)")]
        [SerializeField] private bool _isPureGenerator;   

        [Header("Hybrid Generator (Hover below field for explanation)" )]    
        [Tooltip("Can generate and convert if has correct ingredient combination (CHOOSE ONE ONLY!)")]
        [SerializeField] private bool _isHybridGenerator;
        [SerializeField] private List<GeneratorUtensilStatsData> StatsDataPerLevel;
        private GeneratorUtensilStatsData _currentStatsData;

        protected override void Start()
        {
            base.Start();
            if(_isPureGenerator == _isHybridGenerator)
            {
                Debug.LogError("Pure Generator and Hybrid Generator cannot be the same!");
            }

            if(_isPureGenerator)
            {
                //None for now
            }
            else if(_isHybridGenerator)
            {
                // Need additional check for if player has loading save or not
                _currentStatsData = StatsDataPerLevel[0];
            }

        }

        public override void Interact()
        {
            if(_isPureGenerator)
            {
                if(_playerControllerRef.PlayerInventory.PlayerInventoryList.Count != 0)
                {
                    _playerControllerRef.PlayerInventory.RemoveInventoryAll();
                    _playerControllerRef.PlayerInventory.AddInventory(_generatedIngredient);
                }
            }
            else if(_isHybridGenerator)
            {
                if(_playerControllerRef.PlayerInventory.PlayerInventoryList.Count <= 0) return; //No ingredient to process

                //Give ingredient first
                //Process it by looking at all possible recipe
                //Return new processed ingredient
                ReturnNewFood(_playerControllerRef.PlayerInventory, _generatedIngredient);
                TryProcessInput(_playerControllerRef.PlayerInventory);
            }
        }

        private void TryProcessInput(PlayerInventory playerInventory)
        {
            List<Ingredient> inputtedIngredients = new List<Ingredient>(playerInventory.PlayerInventoryList);
            playerInventory.RemoveInventoryAll();

            int inputSize = inputtedIngredients.Count;

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
                ReturnNewFood(playerInventory, FailedFood);
                return;
            }

            
            foreach(Recipe recipe in recipeWithSameSize)
            {
                //This behavior does care about the order of ingredients
                if(ListComparer.IsEqualWithSameOrder(inputtedIngredients, recipe.RecipeInformation.Requirements))
                {
                    return;
                }
            }

            //If there are no matched recipe
            // Debug.LogWarning("Recipe not founded");
            ReturnNewFood(playerInventory, FailedFood);
            return;
        }

        private void ReturnNewFood(PlayerInventory playerInventory, Ingredient newFood)
        {
            playerInventory.AddInventory(newFood);
        }
    }
}
