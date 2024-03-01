//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/29"
//----------------------------------------------------------------------

using System;
using System.Collections;
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
    public class ConverterUtensil : Utensil
    {
        [Header("Converter Properties")]
        [SerializeField] private List<ConverterUtensilStatsData> StatsDataPerLevel;
        private ConverterUtensilStatsData _currentStatsData;

        private bool _isProcessing;
        private bool _isFoodReady;
        public Ingredient ReadyToTakeFood {get; private set;}
        
        
        protected override void Start()
        {
            base.Start();

            // Need additional check for if player has loading save or not
            _currentStatsData = StatsDataPerLevel[0];
        }

        public override void Interact()
        {
            if(_isProcessing) return;

            if(!_isFoodReady) _isProcessing = TryProcessInput(_playerControllerRef);

            ReturnNewFood(_playerControllerRef, ReadyToTakeFood);
        }

        private bool TryProcessInput(PlayerController player)
        {
            List<Ingredient> inputtedIngredients = new List<Ingredient>(player.PlayerInventory.PlayerInventoryList);

            int inputSize = inputtedIngredients.Count;

            //Get all recipe with same size as inputtedIngredient
            List<Recipe> recipeToSearch = new List<Recipe>();
            foreach (Recipe recipe in _currentStatsData.RecipeList)
            {
                int recipeRequirementSize = recipe.RecipeInformation.Requirements.Count;
                if(recipeRequirementSize == inputSize)
                {
                    recipeToSearch.Add(recipe);
                }
            }

            if(recipeToSearch.Count <= 0) return false;
            
            foreach(Recipe recipe in recipeToSearch)
            {
                //This behavior doesnt care about the order of ingredients
                // if(ListComparer.IsEqualWithoutSameOrder(inputtedIngredients, recipe.RecipeInformation.Requirements))
                // {
                //     Debug.Log("Found the same recipe");
                //     _isProcessing = true;
                //     StartCoroutine(ProcessingFood(recipe));
                //     return true;
                // }

                //This behavior does care about the order of ingredients
                if(ListComparer.IsEqualWithSameOrder(inputtedIngredients, recipe.RecipeInformation.Requirements))
                {
                    Debug.Log("Found the same recipe");
                    _isProcessing = true;
                    StartCoroutine(ProcessingFood(recipe));
                    return true;
                }
            }

            return false;
        }

        private IEnumerator ProcessingFood(Recipe recipeToProccess)
        {
            yield return new WaitForSeconds(_currentStatsData.ConvertingTime);
            
            ReadyToTakeFood = recipeToProccess.RecipeInformation.RecipeOutput;
            _isProcessing = false;
            _isFoodReady = true;
        }

        private void ReturnNewFood(PlayerController player, Ingredient newFood)
        {
            player.PlayerInventory.AddInventory(newFood);
            _isFoodReady = false;
        }

        
    }
}
