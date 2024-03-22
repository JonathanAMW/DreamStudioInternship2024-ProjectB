//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/15"
//----------------------------------------------------------------------

using System;
using UnderworldCafe.CustomerSystem;
using UnityEngine;


namespace UnderworldCafe.WaveSystem
{
    /// <summary>
    /// Class is for wave object for wave system
    /// </summary>
    public class Wave
    {
        #region Dependency Injection 
        private TimeManager _timeManagerRef;
        #endregion

        #region Events
        public event Action OnWaveDoneEvent;
        #endregion

        [SerializeField] private WaveInformationStruct _waveInformation;
        // private Customer[] _customerObjects;

        public int CurrentWaveCustomerLeft { get; private set; }
        public WaveInformationStruct WaveInformation => _waveInformation;

        // //Constructor
        // public Wave(WaveInformationStruct waveInformation, Timer timerRef)
        // {
        //     _timerRef = timerRef;
        //     _waveInformation = waveInformation;

        //     CurrentWaveCustomerLeft = WaveInformation.WaveCustomerInformations.Count;
        // }

        public void Init(WaveInformationStruct waveInformation, TimeManager timeManagerRef)
        {
            _timeManagerRef = timeManagerRef;
            _waveInformation = waveInformation;

            CurrentWaveCustomerLeft = WaveInformation.WaveCustomerInformations.Count;
        }

        public void DeInit()
        {
            _timeManagerRef = null;

            CurrentWaveCustomerLeft = 0;
        }

        // public void StartWave(Transform[] customerSpawnPoints, Timer timerRef)
        // {
        //     _timerRef = timerRef;
        //     CurrentWaveCustomerLeft = WaveInformation.WaveCustomerInformations.Count;

        //     foreach(var customer in WaveInformation.WaveCustomerInformations)
        //     {   
        //         _customerObjects[customer.CustomerChairIndex].Init(customer.CustomerInformation,
        //                                                             customer.CustomerOrderedFood, 
        //                                                             customer.CustomerOrderDuration, 
        //                                                             _timerRef);
        //         _customerObjects[customer.CustomerChairIndex].OnServedEvent += OnCustomerServedEventHandlerMethod;
        //         _customerObjects[customer.CustomerChairIndex].OnOrderDurationEndedEvent += OnCustomerOrderDurationEndedEventHandlerMethod;
        //     }
        // }

        // public void EndWave()
        // {
        //     _customerObjects = null;
        //     _timerRef = null;
            
        //     foreach(var customer in WaveInformation.WaveCustomerInformations)
        //     {   
        //         _customerObjects[customer.CustomerChairIndex].OnServedEvent -= OnCustomerServedEventHandlerMethod;
        //         _customerObjects[customer.CustomerChairIndex].OnOrderDurationEndedEvent -= OnCustomerOrderDurationEndedEventHandlerMethod;
        //         _customerObjects[customer.CustomerChairIndex].DeInit();
        //     }
        // }

        public void OnCustomerOrderDurationEndedEventHandlerMethod()
        {
            _timeManagerRef.AddTimePassed(WaveInformation.WaveTimeDecrementByOrderDurationEnded);
        }

        public void OnCustomerServedEventHandlerMethod(bool isServedCorrectly)
        {
            if(!isServedCorrectly)
            {
                _timeManagerRef.AddTimePassed(WaveInformation.WaveTimeDecrementByWrongServing);
            }
            else
            {
                CurrentWaveCustomerLeft--;
                
                //if all customers in this wave have been served correctly
                if(CurrentWaveCustomerLeft <= 0)
                {
                    OnWaveDoneEvent?.Invoke();
                }
            }
        }

    }
}
