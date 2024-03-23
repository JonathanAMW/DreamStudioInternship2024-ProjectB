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
    public class WaveManager : MonoBehaviour
    {
        #region Dependencies
        PoolManager _poolManagerRef;
        TimeManager _timeManagerRef;
        #endregion

        [System.Serializable]
        public struct CustomerSpawnPointsStruct
        {
            public int pointIndex;
            public Transform pointTransform;
        }

        #region Private
        [Tooltip("Drag the Wave SO here")]
        [SerializeField] private WaveInformationSO[] _waveInformationSOList;
        [Tooltip("Drag the customer object spawn points here")]
        [SerializeField] private CustomerSpawnPointsStruct[] _customerSpawnPointsConfig;
        private Wave _currentWave;
        private int _waveIndex;
        private Queue<GameObject> _usedCustomersFromPool;
        #endregion


        #region MonoBehavior

        private void Awake()
        {
            _poolManagerRef = LevelManager.Instance.PoolManager;
            _timeManagerRef = LevelManager.Instance.TimeManager;

            _usedCustomersFromPool = new();
            _currentWave = new Wave();
            SetupCustomerPool();
        }
        private void Start()
        {
            // //No need to copy as it is not going to be reused
            // _waveList = new Wave[_waveInformationSOList.Length];
            // for(int i = 0; i < _waveInformationSOList.Length; i++)
            // {
            //     _waveList[i] = new Wave(_waveInformationSOList[i].WaveInformation);
            // }

            
            // _poolManagerRef = LevelManager.Instance.LevelPoolManagerRef;

            // _usedCustomersFromPool = new();
            // _currentWave = new Wave();
            // SetupCustomerPool();
        }

        private void OnEnable()
        {
            _timeManagerRef.OnTimerEndedEvent += OnTimerEndedEventHandlerMethod;
            _currentWave.OnWaveDoneEvent += OnCurrentWaveDoneEventHandlerMethod;

            if(_usedCustomersFromPool.Count > 0 || _usedCustomersFromPool != null)
            {
                foreach(var customerObj in _usedCustomersFromPool)
                {
                    if(customerObj == null) continue;

                    customerObj.GetComponent<Customer>().OnServedEvent += _currentWave.OnCustomerServedEventHandlerMethod;
                    customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent += _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;
                }
            }
        }
        private void OnDisable()
        {
            _timeManagerRef.OnTimerEndedEvent -= OnTimerEndedEventHandlerMethod;
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;

            if(_usedCustomersFromPool.Count > 0 || _usedCustomersFromPool != null)
            {
                foreach(var customerObj in _usedCustomersFromPool)
                {
                    if(customerObj == null) continue;
                    
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
                    _poolManagerRef.TryCreateNewPool(waveCustomerInfos.CustomerPrefab.GetComponent<Customer>().CustomerId, waveCustomerInfos.CustomerPrefab);
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
            _currentWave.Init(_waveInformationSOList[_waveIndex].WaveInformation, _timeManagerRef);

            _currentWave.OnWaveDoneEvent += OnCurrentWaveDoneEventHandlerMethod;

            foreach(var customer in _currentWave.WaveInformation.WaveCustomerInformations)
            {
                foreach(var spawnPoint in _customerSpawnPointsConfig)
                {
                    if(spawnPoint.pointIndex == customer.CustomerChairIndex)
                    {
                        var customerObj = _poolManagerRef.TryGetFromPool(customer.CustomerPrefab.GetComponent<Customer>().CustomerId);
                        
                        if(customerObj == null)
                        {
                            Debug.LogWarning("Customer prefab not found: " + customer.CustomerPrefab.name);
                            continue;
                        }
                        
                        customerObj.transform.position = spawnPoint.pointTransform.position;
                        
                        customerObj.GetComponent<Customer>().Init(customer.CustomerOrderedFood, customer.CustomerOrderDuration, _timeManagerRef);
                        customerObj.GetComponent<Customer>().OnServedEvent += _currentWave.OnCustomerServedEventHandlerMethod;
                        customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent += _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;

                        _usedCustomersFromPool.Enqueue(customerObj);

                        break;
                    }

                }        
            }

            if(_usedCustomersFromPool.Count != _currentWave.WaveInformation.WaveCustomerInformations.Count)
            {
                Debug.LogError($"There are still '{_usedCustomersFromPool.Count - _currentWave.WaveInformation.WaveCustomerInformations.Count}' customers that has not been assigned on spawn points!");
            }
        }

        private void EndCurrentWave()
        {
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;
            
            while (_usedCustomersFromPool.Count > 0)
            {
                var customerObj = _usedCustomersFromPool.Dequeue();
                
                bool released = _poolManagerRef.TryReleaseToPool(customerObj.GetComponent<Customer>().CustomerId, customerObj);
                if(!released)
                {
                    Debug.LogWarning("Customer object failed to be released: " + customerObj.name);
                    Destroy(customerObj);
                    continue;
                }

                customerObj.transform.position = new Vector3(0, 0, 0);
                customerObj.GetComponent<Customer>().DeInit();

                customerObj.GetComponent<Customer>().OnServedEvent -= _currentWave.OnCustomerServedEventHandlerMethod;
                customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent -= _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;

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
