//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/22"
//----------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class for managing time in level
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        #region Dependencies
        private GameManager _gameManagerRef;
        private LevelManager _levelManagerRef;
        #endregion
        

        public float TimerDuration { get; private set; }  
        public float TimePassed { get; private set; }
        

        #region MonoBehavior
        private void Awake()
        {
            _gameManagerRef = GameManager.Instance;
            _levelManagerRef = LevelManager.Instance;
        }
        private void OnEnable()
        {
            LevelManager.OnLevelCompletedEvent += OnLevelCompletedEventHandlerMethod;
        }
        private void OnDisable()
        {
            LevelManager.OnLevelCompletedEvent -= OnLevelCompletedEventHandlerMethod;
        }
        #endregion

        public void StartTimer(float timerDuration)
        {
            TimePassed = 0;
            TimerDuration = timerDuration;

            StartCoroutine("TimerCoroutine");
        }
        public void StopTimer()
        {
            StopCoroutine("TimerCoroutine");
        }
        
        public void AddTimePassed(float addedTime)
        {
            TimePassed += addedTime;
        }

        private IEnumerator TimerCoroutine()
        {
            while(TimePassed < TimerDuration)
            {
                if(_gameManagerRef.IsGamePaused) yield return null;
                
                TimePassed += Time.deltaTime;
                // Debug.Log(TimePassed);
                yield return null;
            }

            TimePassed = TimerDuration;
            _levelManagerRef.LevelIsCompleted();
        }

        private void OnLevelCompletedEventHandlerMethod()
        {
            StopTimer();

            // _levelManagerRef.OnLevelCompletedEvent -= OnLevelCompletedEventHandlerMethod;
        }
    }
}
