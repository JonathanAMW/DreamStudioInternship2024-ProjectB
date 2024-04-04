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
        private AudioManager _audioManagerRef;
        private LevelManager _levelManagerRef;
        private TimeManager _timeManagerRef;
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
        private int _currentWaveIndex;
        private Queue<GameObject> _usedCustomersFromPool;
        private Dictionary<string, IObjectPool<GameObject>> _customerObjPool;

        #endregion

        #region Public
        public int CurrentWaveCustomerLeftCount => _currentWave.CurrentWaveCustomerLeft;
        #endregion


        #region MonoBehavior
        private void Awake()
        {
            _audioManagerRef = GameManager.Instance.AudioManager;
            _levelManagerRef = LevelManager.Instance;
            // _poolManagerRef = _levelManagerRef.PoolManager;
            _timeManagerRef = _levelManagerRef.TimeManager;
            
            _usedCustomersFromPool = new();
            _currentWave = new Wave();
            SetupCustomerPool();
        }
        private void OnEnable()
        {
            _levelManagerRef.OnLevelCompletedEvent += OnLevelCompletedEventHandlerMethod;
        }
        #endregion


        #region CustomerObjPool Methods
        private void SetupCustomerPool()
        {
            _customerObjPool = new();

            foreach(WaveInformationSO waveSO in _waveInformationSOList)
            {
                foreach(var waveCustomerInfos in waveSO.WaveInformation.WaveCustomerInformations)
                {
                    if(!_customerObjPool.ContainsKey(waveCustomerInfos.CustomerPrefab.GetComponent<Customer>().CustomerId))
                    {
                        _customerObjPool.Add(waveCustomerInfos.CustomerPrefab.GetComponent<Customer>().CustomerId, 
                                            new ObjectPool<GameObject>(() => Instantiate<GameObject>(waveCustomerInfos.CustomerPrefab), null, null));
                    }
                }
            }
        }
        private GameObject TryGetFromCustomerObjPool(string customerId)
        {
            if(!_customerObjPool.ContainsKey(customerId))
            {
                Debug.LogError($"CustomerObj pool with ID '{customerId}' does not exist. Failed to get object.");
                return null;
            }

            var obj = _customerObjPool[customerId].Get();
            obj.SetActive(true);
            return obj;
        }
        private void TryReleaseToCustomerObjPool(string customerId, GameObject obj)
        {
            if (!_customerObjPool.ContainsKey(customerId))
            {
                Debug.LogWarning($"CustomerObj pool with ID '{customerId}' does not exist. Failed to release object.");
                Destroy(obj);
                return;
            }
            obj.SetActive(false);
            _customerObjPool[customerId].Release(obj);
        }
        
        #endregion


        public void StartWaveSequence()
        {
            _currentWaveIndex = 0;

            // _currentWave = new Wave(_waveInformationSOList[_waveIndex].WaveInformation, _timerRef);

            StartCurrentWave();
        }
        public void ContinueWaveSequence()
        {
            EndCurrentWave();
            
            _currentWaveIndex++;

            if(_currentWaveIndex < _waveInformationSOList.Length)
            {
                StartCurrentWave();
            }
            else
            {
                _levelManagerRef.LevelIsCompleted();
                // EndWaveSequence();
            }
        }
        public void EndWaveSequence()
        {
            EndCurrentWave();
        }

        private void StartCurrentWave()
        {
            Debug.Log("Start Wave: " + _currentWaveIndex);

            _currentWave.Init(_waveInformationSOList[_currentWaveIndex].WaveInformation, _timeManagerRef);

            _currentWave.OnWaveDoneEvent += OnCurrentWaveDoneEventHandlerMethod;

            Debug.Log("Expected Spawned customers: " + _currentWave.WaveInformation.WaveCustomerInformations.Count);
            foreach(var customer in _currentWave.WaveInformation.WaveCustomerInformations)
            {
                foreach(var spawnPoint in _customerSpawnPointsConfig)
                {
                    if(spawnPoint.pointIndex == customer.CustomerSpawnPointIndex)
                    {
                        var customerObj = TryGetFromCustomerObjPool(customer.CustomerPrefab.GetComponent<Customer>().CustomerId);
                        
                        if(customerObj == null)
                        {
                            continue;
                        }
                        
                        customerObj.transform.position = spawnPoint.pointTransform.position;
                        
                        customerObj.GetComponent<Customer>().Init(customer.CustomerOrderedFood, customer.CustomerOrderDuration, _timeManagerRef);
                        customerObj.GetComponent<Customer>().OnServedEvent += _currentWave.OnCustomerServedEventHandlerMethod;
                        customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent += _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;

                        _usedCustomersFromPool.Enqueue(customerObj);

                        Debug.Log("Spawned customer: " + customerObj.name);
                        _audioManagerRef.PlaySFX(_audioManagerRef.CustomerSpawnSFX);
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
            Debug.Log("End Wave: " + _currentWaveIndex);
            _currentWave.OnWaveDoneEvent -= OnCurrentWaveDoneEventHandlerMethod;
            
            while (_usedCustomersFromPool.Count > 0)
            {
                var customerObj = _usedCustomersFromPool.Dequeue();

                customerObj.transform.position = new Vector3(0, 0, 0);
                customerObj.GetComponent<Customer>().DeInit();

                customerObj.GetComponent<Customer>().OnServedEvent -= _currentWave.OnCustomerServedEventHandlerMethod;
                customerObj.GetComponent<Customer>().OnOrderDurationEndedEvent -= _currentWave.OnCustomerOrderDurationEndedEventHandlerMethod;

                TryReleaseToCustomerObjPool(customerObj.GetComponent<Customer>().CustomerId, customerObj);
            }

            _currentWave.DeInit();
        }

        private void OnCurrentWaveDoneEventHandlerMethod()
        {
            ContinueWaveSequence();
        }

        private void OnLevelCompletedEventHandlerMethod()
        {
            EndWaveSequence();

            _levelManagerRef.OnLevelCompletedEvent -= OnLevelCompletedEventHandlerMethod;
        }
    }
}
