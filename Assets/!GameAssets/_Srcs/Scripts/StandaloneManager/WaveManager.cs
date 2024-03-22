//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/14"
//----------------------------------------------------------------------

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

using UnderworldCafe.CustomerSystem;

namespace UnderworldCafe.WaveSystem
{
    /// <summary>
    /// Class is for managing the wave sequence or wave-line of the level
    /// </summary>
    public class WaveManager : DestroyOnLoadSingletonMonoBehaviour<WaveManager>
    {
        #region Dependency Injection
        PoolManager _poolManagerRef => PoolManager.Instance;
        Timer _timerRef;
        #endregion


        #region Private
        [Tooltip("Drag the Wave SO here")]
        [SerializeField] private WaveInformationSO[] _waveInformationSOList;
        [Tooltip("Drag the customer object spawn points here")]
        [SerializeField] private Transform[] _customerChairSpawnPointsInScene;
        private Wave _currentWave;
        private int _waveIndex;
        private Queue<GameObject> _usedCustomersFromPool;
        #endregion


        #region MonoBehavior
        private void Start()
        {
            // //No need to copy as it is not going to be reused
            // _waveList = new Wave[_waveInformationSOList.Length];
            // for(int i = 0; i < _waveInformationSOList.Length; i++)
            // {
            //     _waveList[i] = new Wave(_waveInformationSOList[i].WaveInformation);
            // }

            _usedCustomersFromPool = new();
            _currentWave = new Wave();
            SetupCustomerPool();
        }

        private void OnEnable()
        {
            _timerRef.OnTimerEndedEvent += OnTimerEndedEventHandlerMethod;
            _currentWave.OnWaveDoneEvent += OnCurrentWaveDoneEventHandlerMethod;

            if(_usedCustomersFromPool.Count > 0 || _usedCustomersFromPool != null)
            {
                foreach(var customerObj in _usedCustomersFromPool)
                {
                    customerObj.GetComponent<Customer>().OnServedEvent += _currentWave.OnCustomerServedEventHandlerMethod;
                    customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent += _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;
                }
            }
        }
        private void OnDisable()
        {
            _timerRef.OnTimerEndedEvent -= OnTimerEndedEventHandlerMethod;
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;

            if(_usedCustomersFromPool.Count > 0 || _usedCustomersFromPool != null)
            {
                foreach(var customerObj in _usedCustomersFromPool)
                {
                    customerObj.GetComponent<Customer>().OnServedEvent -= _currentWave.OnCustomerServedEventHandlerMethod;
                    customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent -= _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;
                }
            }
        }
        #endregion


        #region CustomerObj Pool
        private void SetupCustomerPool()
        {
            foreach(WaveInformationSO waveSO in _waveInformationSOList)
            {
                foreach(var waveCustomerInfos in waveSO.WaveInformation.WaveCustomerInformations)
                {
                    _poolManagerRef.TryAddToPool(waveCustomerInfos.CustomerPrefab.GetComponent<Customer>().CustomerId, waveCustomerInfos.CustomerPrefab);
                }
            }
        }
        #endregion


        public void StartWaveSequence()
        {
            _waveIndex = 0;

            // _currentWave = new Wave(_waveInformationSOList[_waveIndex].WaveInformation, _timerRef);

            StartCurrentWave();
        }

        private void ContinueWaveSequence()
        {
            if(_waveIndex < _waveInformationSOList.Length)
            {
                // _currentWave.EndWave();
                EndCurrentWave();
                
                _waveIndex++;
                // _currentWave = new Wave(_waveInformationSOList[_waveIndex].WaveInformation, _timerRef);

                // _currentWave.StartWave(_customerObjectSpawnPointsInScene, _timerRef);
                StartCurrentWave();
            }
            else
            {
                EndWaveSequence();
            }
        }

        public void EndWaveSequence()
        {
            // _currentWave.EndWave();
            EndCurrentWave();
        }

        private void StartCurrentWave()
        {
            _currentWave.Init(_waveInformationSOList[_waveIndex].WaveInformation, _timerRef);

            _currentWave.OnWaveDoneEvent += OnCurrentWaveDoneEventHandlerMethod;

            foreach(var customer in _currentWave.WaveInformation.WaveCustomerInformations)
            {
                // skip if customer chair index is out of bounds
                if(customer.CustomerChairIndex >= _customerChairSpawnPointsInScene.Length || customer.CustomerChairIndex < 0)
                {
                    continue;
                }

                var customerObj = _poolManagerRef.TryGetFromPool(customer.CustomerPrefab.GetComponent<Customer>().CustomerId);

                
                customerObj.transform.position = _customerChairSpawnPointsInScene[customer.CustomerChairIndex].position;
                customerObj.GetComponent<Customer>().Init(customer.CustomerOrderedFood, customer.CustomerOrderDuration, _timerRef);

                customerObj.GetComponent<Customer>().OnServedEvent += _currentWave.OnCustomerServedEventHandlerMethod;
                customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent += _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;


                _usedCustomersFromPool.Enqueue(customerObj);
            }
        }

        private void EndCurrentWave()
        {
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;
            
            for(int i = 0; i < _usedCustomersFromPool.Count; i++)
            {
                var customerObj = _usedCustomersFromPool.Peek();
                
                _poolManagerRef.TryReleaseToPool(customerObj.GetComponent<Customer>().CustomerId, customerObj);

                customerObj.transform.position = new Vector3(0, 0, 0);
                customerObj.GetComponent<Customer>().DeInit();

                customerObj.GetComponent<Customer>().OnServedEvent -= _currentWave.OnCustomerServedEventHandlerMethod;
                customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent -= _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;

                _usedCustomersFromPool.Dequeue();
            }

            _currentWave.DeInit();
        }

        private void OnCurrentWaveDoneEventHandlerMethod()
        {
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;
            ContinueWaveSequence();
        }

        private void OnTimerEndedEventHandlerMethod()
        {
            EndWaveSequence();
        }

        public int GetCustomerLeftCount()
        {
            return _currentWave.CurrentWaveCustomerLeft;
        }
    }
}
