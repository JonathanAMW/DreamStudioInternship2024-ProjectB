//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/09"
//----------------------------------------------------------------------


using UnityEngine;
using UnityEngine.InputSystem;

using UnderworldCafe.CookingSystem;
using UnderworldCafe.GameManageralSystem;

namespace UnderworldCafe.InputSystem
{
    /// <summary>
    /// This class will be used to handle input events from player
    /// </summary>
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private LevelManager _levelManagerRef;
        private Camera _mainCameraRef;
        private GameInputAction _gameInputAction;

        [SerializeField] private LayerMask _clickableLayerMask;

        // #region event
        // public delegate void ClickOrPressEventHandler(Vector2 position);
        // public event ClickOrPressEventHandler OnClickOrPressEvent;
        // #endregion


        protected override void Awake()
        {
            base.Awake();

            _gameInputAction = new GameInputAction();
            _mainCameraRef = Camera.main;    
        }

        private void Start()
        {
            _gameInputAction.Gameplay.Press.started += ctx => ClickOrPress(ctx);
        }

        private void OnEnable()
        {
            _gameInputAction.Enable();
        }

        private void OnDisable()
        {   
            _gameInputAction.Disable();
        }


        private void ClickOrPress(InputAction.CallbackContext context)
        {
            Ray ray = _mainCameraRef.ScreenPointToRay(_gameInputAction.Gameplay.PointerPosition.ReadValue<Vector2>());
            
            // Raycast arguments is bad in term it cant differentiate between distance and layemask
            // which made me include the Mathf.Infinity arg so it can use LayerMask correctly
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _clickableLayerMask);

            if(hit2D.collider == null) return;
            if(hit2D.collider.gameObject.GetComponent<IInteractable>() == null)
            {
                Debug.LogWarning("The interactable object is detected but its script is not correctly placed!");
                return;
            }

            IInteractable interactedObj = hit2D.collider.gameObject.GetComponent<IInteractable>();

            // Check type of interactable object
            if(interactedObj is QueuedInteractableObject)
            {
                ((QueuedInteractableObject)interactedObj).TryInteract();
            }
            else
            {
                Debug.LogError("Not Implemented: " + interactedObj.GetType());
            }
        }
    }
}
