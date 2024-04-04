//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/10"
//----------------------------------------------------------------------

using System;
using System.Collections;

using UnityEngine;

using UnderworldCafe.CookingSystem;
using UnderworldCafe.Player;
using UnityEngine.Pool;


namespace UnderworldCafe.CustomerSystem
{
    /// <summary>
    /// Class for customer gameobject
    /// </summary>
    public class Customer : QueuedInteractableObject
    {
        #region Dependencies
        private TimeManager _timeManagerRef;
        #endregion


        #region Events
        public event Action<Customer, bool> OnServedEvent;
        public event Action OnOrderDurationEndedEvent;
        #endregion


        [System.Serializable]
        private enum CustomerState
        {
            NONE = 0,
            READY_TO_ORDER,
            WAITING_FOR_FOOD
        }


        #region Private
        [Header("Customer Information")]
        [SerializeField] private string _customerId;
        [SerializeField] private string _customerName;

        [Header("Customer Visuals")]
        [SerializeField] private Animator _customerSpriteAnimator;
        [SerializeField] private SpriteRenderer _customerSpriteRenderer;
        
        [Header("Customer UI")]
        [SerializeField] private GameObject _orderObj;
        [SerializeField] private SpriteRenderer _orderFoodSpriteRenderer;
        
        private CustomerState _customerState;
        #endregion


        #region Public
        public bool IsServedCorrectly { get; private set; }
        public float OrderDuration { get; private set; }
        public Ingredient OrderedFood { get; private set; }
        public string CustomerId => _customerId;
        #endregion


        protected override void OnValidate()
        {
            base.OnValidate();

            if(String.IsNullOrEmpty(_customerName))
            {
                Debug.LogWarning("No customer name has been set on " + gameObject.name);
            }
            
            _customerId = GetType().Name.ToUpper() + "_" + _customerName.Replace(" ", "");
        }

        public override void Interact()
        {
            if(_customerState == CustomerState.READY_TO_ORDER)
            {
                StartOrderingFood();
            }
            else if(_customerState == CustomerState.WAITING_FOR_FOOD)
            {
                ServedFood(_playerControllerRef.PlayerInventory);
            }
        }

        public void Init(Ingredient orderedFood, float orderDuration, TimeManager timeManagerRef)
        {
            _timeManagerRef = timeManagerRef;

            //set variables
            IsServedCorrectly = false;
            OrderDuration = orderDuration;
            OrderedFood = orderedFood;
            _customerState = CustomerState.READY_TO_ORDER;

            _orderObj.SetActive(false);
        }

        public void DeInit()
        {
            StopAllCoroutines();

            _timeManagerRef = null;

            IsServedCorrectly = false;
            OrderDuration = 0.0f;
            OrderedFood = null;
            _customerState = CustomerState.NONE;

            _orderObj.SetActive(false);
        }

        private void StartOrderingFood()
        {
            _customerState = CustomerState.WAITING_FOR_FOOD;

            //set order visual
            _orderFoodSpriteRenderer.sprite = OrderedFood.IngredientInformation.IngredientSprite;
            _orderObj.SetActive(true);

            StartCoroutine("WaitingForFood");
        }

        private IEnumerator WaitingForFood()
        {
            float startOrderTime = _timeManagerRef.TimePassed;

            while(_timeManagerRef.TimePassed - startOrderTime < OrderDuration)
            {
                yield return null;
            }

            OnOrderDurationEndedEvent?.Invoke();
        }

        private void ServedFood(PlayerInventory playerInventory)
        {
            if(playerInventory == null || playerInventory.PlayerInventoryList.Count == 0) return;

            bool IsServedCorrectly = playerInventory.PlayerInventoryList[0].IngredientInformation.Id == OrderedFood.IngredientInformation.Id;

            playerInventory.RemoveInventoryAll();

            if(IsServedCorrectly)
            {
                DeInit();
            }

            OnServedEvent?.Invoke(this, IsServedCorrectly);
        }
    }
}
