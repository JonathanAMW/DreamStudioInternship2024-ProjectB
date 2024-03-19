//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnderworldCafe.GridSystem;
using UnderworldCafe.Player;
using UnderworldCafe.DataPersistenceSystem.Interfaces;
using UnderworldCafe.DataPersistenceSystem.GameDatas;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
    /// </summary>
    public abstract class Utensil : MonoBehaviour, IInteractable
    {
        // Region for every class that is referenced to be used within this class
        #region References Dependency
        protected PlayerController _playerControllerRef => PlayerController.Instance;
        protected GridManager _gridManagerRef => GridManager.Instance;
        #endregion
        

        #region Utensil Information
        [Header("=======[Utensil Information]=======")] 

        [SerializeField] private UtensilInformation _utensilInformation;
        public UtensilInformation UtensilInformations => _utensilInformation;

        
        [System.Serializable]
        public struct UtensilInformation
        {
            public string Name;

            [TextArea]
            public string Description;
        }
        #endregion


        #region Utensil Properties 
        [Header("=======[Utensil General Properties]=======")]        
        
        
        [Header("Utensil Failed Food")]
        [SerializeField] protected Ingredient FailedFood;


        [Header("Utensil Visual")]
        [SerializeField] private Transform _interactPos;
        private bool _isSelected;
        [SerializeField] private GameObject _selectedIndicator;
        private int _indexInQueue;
        [SerializeField] private TextMeshProUGUI _indexInQueueText;
        [SerializeField] private Animator _utensilAnimator;
        #endregion
            
        protected virtual void OnValidate()
        {
            if(string.IsNullOrEmpty(_utensilInformation.Name))
            {
                Debug.LogWarning("No name has been set on utensil: " + gameObject.name);
            }
            if(string.IsNullOrEmpty(_utensilInformation.Description))
            {
                Debug.LogWarning("No description has been set on utensil: " + gameObject.name);
            }
            if(FailedFood == null)
            {
                Debug.LogWarning("No failed food has been set on utensil: " + gameObject.name);
            }
            if(_interactPos == null)
            {
                Debug.LogWarning("No interact position has been set on utensil: " + gameObject.name);
            }
            if(_selectedIndicator == null)
            {
                Debug.LogWarning("No selected indicator has been set on utensil: " + gameObject.name);
            }
            if(_indexInQueueText == null)
            {
                Debug.LogWarning("No index in queue text has been set on utensil: " + gameObject.name);
            }
            // if(_utensilAnimator == null)
            // {
            //     Debug.LogWarning("No animator has been set on utensil: " + gameObject.name);
            // }
            
        }

        protected virtual void Start() 
        {
            ChangeSelectedState(false);
        }
        
        public abstract void Interact();
        

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
            _indexInQueueText.text = _indexInQueue.ToString();
        }


        protected static class UtensilComparer
        {
            public static bool IsEqualWithSameOrder<T>(IEnumerable<T> list1, IEnumerable<T> list2, Func<T, object> propertySelector)
            {
                if(list1.Count() != list2.Count()) return false;

                
                // Get enumerators for both lists
                var enumerator1 = list1.GetEnumerator();
                var enumerator2 = list2.GetEnumerator();

                // Iterate over the elements of both lists
                while (enumerator1.MoveNext() && enumerator2.MoveNext())
                {
                    // Get the selected property for the current elements
                    var property1 = propertySelector(enumerator1.Current);
                    var property2 = propertySelector(enumerator2.Current);

                    // Compare the selected properties
                    if (!property1.Equals(property2))
                    {
                        // If properties are not equal, return false
                        return false;
                    }
                }

                // If all properties are equal, return true
                return true;
            }
        }
    }
}
