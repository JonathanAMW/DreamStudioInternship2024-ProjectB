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
        [Header("=======[Converter Utensil Properties]=======")]
        [SerializeField] private List<ConverterUtensilStatsData> _statsDataPerLevel;
        public List<ConverterUtensilStatsData> StatsDataPerLevel => _statsDataPerLevel;

        private ConverterUtensilStatsData _currentStatsData;

        private bool _isProcessing;
        private bool _isFoodReady;
        public Ingredient ReadyToTakeFood {get; private set;}
        

        protected override void Start()
        {
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
                _isProcessing = TryProcessInput(_playerControllerRef);
                return;
            }

            ReturnNewFood(_playerControllerRef, ReadyToTakeFood);
        }

        private bool TryProcessInput(PlayerController player)
        {
            if(player.PlayerInventory.PlayerInventoryList.Count <= 0) return false;

            List<Ingredient> inputtedIngredients = new List<Ingredient>(player.PlayerInventory.PlayerInventoryList);
            player.PlayerInventory.RemoveInventoryAll();

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

            //If there are no matched recipe
            if(recipeWithSameSize.Count <= 0)
            {
                ReturnNewFood(player, FailedFood);
                return false;
            }

            
            foreach(Recipe recipe in recipeWithSameSize)
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
                    Debug.LogWarning("Found the same recipe");
                    _isProcessing = true;
                    StartCoroutine(ProcessingFood(recipe.RecipeInformation.RecipeOutput));
                    return true;
                }
            }

            //If there are no matched recipe
            Debug.LogWarning("Recipe not founded");
            ReturnNewFood(player, FailedFood);
            return false;
        }

        private IEnumerator ProcessingFood(Ingredient createdIngredient)
        {
            yield return new WaitForSeconds(_currentStatsData.ConvertingTime);
            
            ReadyToTakeFood = createdIngredient;
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
