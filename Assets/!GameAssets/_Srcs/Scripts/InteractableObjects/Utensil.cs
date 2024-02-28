//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/10/19"
//----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UtilityCollections;

using UnderworldCafe.GridSystem;
using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem.Interfaces;
using UnderworldCafe.DataPersistenceSystem.GameDatas;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
    /// </summary>
    public class Utensil : MonoBehaviour, IInteractable
    {
        private PlayerController _playerControllerRef => PlayerController.Instance;
        private GridManager _gridManagerRef => GridManager.Instance;

        
        
        [SerializeField] private Transform _interactPos;
        //Indicator
        private bool _isSelected;
        [SerializeField] private GameObject _selectedIndicator;
        private int _indexInQueue;
        [SerializeField] private TextMeshProUGUI _indexInQueueTMPro;
        //recipe
        // private List<Ingredient> _ingredientsInput;
        

        #region SourceUtensil
        [Header("Utensil As Generator")]
        [Header("Select this if the utensil should generate resource! (e.g. )")]
        [SerializeField] private bool _isGenerator;
        [SerializeField] private Ingredient _generatedIngredient;

        #endregion


        #region ConvertUtensil
        [Header("Utensil As Converter")]
        [Header("Select this if the utensil should convert resource (e.g. combine, mix, etc.)!")]
        [SerializeField] private bool _isConverter;
        [SerializeField] private List<Recipe> _recipeList;

        #endregion
        

        private void Start() 
        {
            ChangeSelectedState(false);
        }
        

        public virtual void Interact()
        {
            if(_isGenerator)
            {

            }   
            else if(_isConverter)
            {

            }
            else
            {
                Debug.Log("");
            }
        }

        private void TryProcessInput(List<Ingredient> inputtedIngredients)
        {
            int inputSize = inputtedIngredients.Count;

            //Get all recipe with same size as inputtedIngredient
            List<Recipe> recipeToSearch = new List<Recipe>();
            foreach (Recipe recipe in _recipeList)
            {
                int recipeRequirementSize = recipe.RecipeInformation.Requirements.Count;
                if(recipeRequirementSize == inputSize)
                {
                    recipeToSearch.Add(recipe);
                }
            }

            if(recipeToSearch.Count <= 0) return;
            
            foreach(Recipe recipe in recipeToSearch)
            {
                if(ListComparer.AreListsSame(inputtedIngredients, recipe.RecipeInformation.Requirements))
                {
                    Debug.Log("Found the same recipe");

                    // Coroutine 
                }
            }
        }


        private IEnumerator ProcessingFood(Recipe recipeToProccess)
        {
            yield return null;
        }

        // private Ingredient ReturnNewFood()
        // {

        // }


        

        public void TrySelectThisUtensil()
        {
            if(!_isSelected)
            {
                _playerControllerRef.CreatePlayerMovementRequest(_interactPos.position, OnMovementDone, OnRequestAccepted, AssignUtensilIndex, this);
            }
            else
            {
                _playerControllerRef.RemovePlayerMovementRequest(this);
            }
        }

        private void OnMovementDone(bool success)
        {
            if(!success) return;


            ChangeSelectedState(false);

            Interact();
        }

        private void OnRequestAccepted(bool accepted)
        {
            ChangeSelectedState(accepted);
        }


        private void ChangeSelectedState(bool state)
        {
            _isSelected = state;
            _selectedIndicator.gameObject.SetActive(state);
        }

        public void AssignUtensilIndex(int assignedIndex)
        {
            _indexInQueue = assignedIndex;
            _indexInQueueTMPro.text = _indexInQueue.ToString();
        }

        
    }
}
