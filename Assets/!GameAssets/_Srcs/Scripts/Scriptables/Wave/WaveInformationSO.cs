using System.Collections.Generic;
using UnityEngine;

using UnderworldCafe.CustomerSystem;
using UnderworldCafe.CookingSystem;


namespace UnderworldCafe.WaveSystem
{
    /// <summary>
    /// Class is for defining wave information using SO for Wave class in WaveSystem
    /// </summary>
    [CreateAssetMenu(fileName = "[WaveInformationName]_SO", menuName ="ScriptableObjects/WaveInformationSO")]
    public class WaveInformationSO : ScriptableObject
    {
        [SerializeField] private WaveInformationStruct _waveInformation;
        public WaveInformationStruct WaveInformation => _waveInformation;
    }
    

    [System.Serializable]
    public struct WaveInformationStruct
    {
        [Header("Wave Settings")]
        public float WaveTimeDecrementByWrongServing;
        public float WaveTimeDecrementByOrderDurationEnded;

        [Header("Customer In Wave Settings")]
        [Tooltip("Wave number/order represented by index in wave list")]
        public List<WaveCustomerInformationStruct> WaveCustomerInformations;


        [System.Serializable]
        public struct WaveCustomerInformationStruct
        {
            public int CustomerChairIndex;
            public GameObject CustomerPrefab;
            public Ingredient CustomerOrderedFood;
            public float CustomerOrderDuration;
        }
    }
}
