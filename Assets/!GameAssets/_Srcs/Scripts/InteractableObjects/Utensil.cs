//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
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
    public abstract class Utensil : MonoBehaviour, IInteractable
    {
        // Region for every class that is referenced to be used within this class
        #region References Dependency
        protected PlayerController _playerControllerRef => PlayerController.Instance;
        protected GridManager _gridManagerRef => GridManager.Instance;
        #endregion
        

        #region Utensil Information
        [Header("=======[Utensil Information]=======")]        
        [SerializeField] private UtensilInformation _utensilInformations;
        public UtensilInformation UtensilInformnations => _utensilInformations;

        [System.Serializable]
        public struct UtensilInformation
        {
            public string Name;
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

        
    }
}
