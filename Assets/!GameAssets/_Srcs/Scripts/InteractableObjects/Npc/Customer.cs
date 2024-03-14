//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/10"
//----------------------------------------------------------------------

using UnityEngine;

using UnderworldCafe.CookingSystem;
using System.Collections;

namespace UnderworldCafe.CustomerSystem
{
    /// <summary>
    /// Class for defining customer
    /// </summary>
    public class Customer : MonoBehaviour
    {
        #region Dependency References
        //Timer should be able to have synchronize method 
        //so other that depend on it will be synchronized
        //Then we should determine how will we get the timer
        Timer _timerRef;
        #endregion
        

        #region Private
        [Header("Customer Visuals")]
        [SerializeField] private Animator _npcAnimator;
        [SerializeField] private GameObject _orderVisualObject;
        [SerializeField] private Sprite _orderVisualSprite;

        private float _startOrderTime;
        private bool _isServed;
        private bool _isWaitingForFood;
        #endregion


        #region Public
        public bool IsServedCorrectly {get; private set;}
        public float OrderDuration { get; private set; }
        public Ingredient OrderedFood { get; private set; }
        #endregion


        // Start is called before the first frame update
        private void Start()
        {
            _isServed = false;
            _isWaitingForFood = false;
            _orderVisualObject.SetActive(false);

            IsServedCorrectly = false;
        }

        public void Init()
        {
            _orderVisualSprite = OrderedFood.IngredientInformation.IngredientSprite;
            _orderVisualObject.SetActive(true);
        }

        public void CopyAndSettingUpCustomer(Animator npcAnimator, float orderDuration, Ingredient orderedFood)
        {
            _npcAnimator = npcAnimator;

            OrderDuration = orderDuration;
            OrderedFood = orderedFood;
        }

        public void ResetCustomer()
        {
            _startOrderTime = 0.0f;
            _isServed = false;
            _isWaitingForFood = false;

            IsServedCorrectly = false;
            OrderDuration = 0.0f;
            OrderedFood = null;

            gameObject.SetActive(false);
        }

        public void StartOrderingFood()
        {
            _startOrderTime = _timerRef.TimePassed;
            StartCoroutine(WaitingForFood());
        }

        private IEnumerator WaitingForFood()
        {
            _isWaitingForFood = true;

            while(!_isServed)
            {
                if(_timerRef.TimePassed - _startOrderTime >= OrderDuration)
                {
                    _isWaitingForFood = false;
                    IsServedCorrectly = false;
                }

                yield return null;
            }
        }

        public void ServedFood(Ingredient servedFood)
        {
            // if(!_isWaitingForFood) return;

            StopCoroutine(WaitingForFood());
            _isServed = true;

            IsServedCorrectly = servedFood == OrderedFood ? true : false;
        }


    }
}
