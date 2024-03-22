//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/19"
//----------------------------------------------------------------------

using UnityEngine;
using TMPro;

using UnderworldCafe.Player;

namespace UnderworldCafe
{
    /// <summary>
    /// Class define properties for every interactable object that can be queued for player movement
    /// </summary>
    public abstract class QueuedInteractableObject : MonoBehaviour, IInteractable
    {
        #region Dependency Injection
        protected PlayerController _playerControllerRef;
        #endregion 

        [Header("[Queued Interactable Object Settings]")]
        [SerializeField] private Transform _interactPos;
        [SerializeField] private GameObject _queuedIndicator;
        [SerializeField] private Animator _queuedIndicatorAnimator;
        [SerializeField] private TextMeshProUGUI _queuedIndexText;
        
        private bool _isQueued;
        private int _queuedIndex;

        protected virtual void OnValidate()
        {
            if(_interactPos == null)
            {
                Debug.LogWarning("No interact position has been set on queued interactable object: " + gameObject.name);
            }
            if(_queuedIndicator == null)
            {
                Debug.LogWarning("No queued indicator has been set on queued interactable object: " + gameObject.name);
            }
            // if(_queuedIndicatorAnimator == null)
            // {
            //     Debug.LogWarning("No queued indicator animator has been set on queued interactable object: " + gameObject.name);
            // }
            if(_queuedIndexText == null)
            {
                Debug.LogWarning("No queued index text has been set on queued interactable object: " + gameObject.name);
            }
        }
        
        protected virtual void Start() 
        {
            ChangeQueuedState(false);

            _playerControllerRef = LevelManager.Instance.LevelPlayerController;
        }        
        
        public abstract void Interact();

        public void TryInteract()
        {
            if(!_isQueued)
            {
                _playerControllerRef.CreatePlayerMovementRequest(_interactPos.position, OnMovementDone, OnRequestAccepted, AssignQueuedIndex, this);
            }
            else
            {
                _playerControllerRef.RemovePlayerMovementRequest(this);
            }
        }

        private void OnMovementDone(bool success)
        {
            if(!success) return;

            ChangeQueuedState(false);

            Interact();
        }

        private void OnRequestAccepted(bool accepted)
        {
            ChangeQueuedState(accepted);
        }

        private void ChangeQueuedState(bool state)
        {
            _isQueued = state;
            _queuedIndicator.gameObject.SetActive(state);
        }

        private void AssignQueuedIndex(int assignedIndex) // before this was public
        {
            _queuedIndex = assignedIndex;
            _queuedIndexText.text = _queuedIndex.ToString();
        }
        
    }
}
