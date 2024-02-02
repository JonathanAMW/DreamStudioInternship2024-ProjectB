//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/10/19"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using TMPro;
using UnderworldCafe.GridSystem;
using UnderworldCafe.Player;
using UnityEngine;


namespace UnderworldCafe.UtensilSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
    /// </summary>
    public abstract class Utensil : MonoBehaviour, IInteractable
    {
        private PlayerController _playerControllerRef => PlayerController.Instance;
        private GridManager _gridManagerRef => GridManager.Instance;
        [SerializeField] private Transform _interactPos;

        //Indicator
        private bool _isSelected;
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private TextMeshProUGUI _indexInQueueTMPro;
        private int _indexInQueue;


        private void Awake() 
        {
            ChangeSelectedState(false);
        }

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

        public abstract void Interact();
    }
}
