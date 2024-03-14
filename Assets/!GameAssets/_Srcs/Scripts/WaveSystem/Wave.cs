
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnderworldCafe.CustomerSystem;
using UnderworldCafe.CookingSystem;


namespace UnderworldCafe.WaveSystem
{
    /// <summary>
    /// Class is for defining wave information using SO of wave in WaveSystem
    /// </summary>
    [CreateAssetMenu(fileName = "[WaveName]_SO", menuName ="ScriptableObjects/WaveSO")]
    public class WaveSO : MonoBehaviour
    {
        [System.Serializable]
            public struct WaveCustomerStruct
            {
                public Customer Customer;
                public Ingredient CustomerOrderedFood;
                public float CustomerOrderDuration;

            public WaveCustomerStruct(Customer customer, Ingredient customerOrderedFood, float customerOrderDuration)
            {
                Customer = customer;
                CustomerOrderedFood = customerOrderedFood;
                CustomerOrderDuration = customerOrderDuration;
            }
        }

        [SerializeField] private List<WaveCustomerStruct> _waveCustomers;
        

        public List<WaveCustomerStruct> WaveCustomers => _waveCustomers;
        
        public bool IsWaveDone { get; private set; }

        public void StartWave()
        {
            foreach(var customerStruct in _waveCustomers)
            {
                
            }

            StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            
            yield return null;
        }


    }
}
