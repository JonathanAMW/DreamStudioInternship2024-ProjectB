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
        #region Dependency Injection
        TimeManager _timeManagerRef;
        #endregion

        #region Events
        public event Action<bool> OnServedEvent;
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

        public static IObjectPool<Customer> CustomerPool { get; private set; }
        public string CustomerId => _customerId;
        #endregion


        protected override void OnValidate()
        {
            base.OnValidate();
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

            //set the gameobject active
            gameObject.SetActive(true);
        }

        public void DeInit()
        {
            StopAllCoroutines();

            _timeManagerRef = null;

            IsServedCorrectly = false;
            OrderDuration = 0.0f;
            OrderedFood = null;

            _customerState = CustomerState.NONE;

            _orderFoodSpriteRenderer.sprite = null;
            _orderObj.SetActive(false);

            gameObject.SetActive(false);
        }

        private void StartOrderingFood()
        {
            _customerState = CustomerState.WAITING_FOR_FOOD;

            //set order visual
            _orderFoodSpriteRenderer.sprite = OrderedFood.IngredientInformation.IngredientSprite;
            _orderObj.SetActive(true);


            StartCoroutine(WaitingForFood());
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
            // if(!_isWaitingForFood) return;

            bool IsServedCorrectly = playerInventory.PlayerInventoryList[0].IngredientInformation.Id == OrderedFood.IngredientInformation.Id ? true : false;

            playerInventory.RemoveInventoryAll();

            OnServedEvent?.Invoke(IsServedCorrectly);

            if(IsServedCorrectly)
            {
                DeInit();
            }
        }
    }
}
