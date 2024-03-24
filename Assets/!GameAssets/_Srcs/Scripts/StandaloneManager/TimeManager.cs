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
        private LevelManager _levelManagerRef;
        #endregion
        

        public bool IsTimerPaused { get; private set; }
        public float TimerDuration { get; private set; }  
        public float TimePassed { get; private set; }
        

        #region MonoBehavior
        private void Awake()
        {
            _levelManagerRef = LevelManager.Instance;
        }
        private void Start()
        {
            _levelManagerRef.OnLevelCompletedEvent += OnLevelCompletedEventHandlerMethod;
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
        public void SetPauseTimer(bool isPaused)
        {
            IsTimerPaused = isPaused;
        }
        public void AddTimePassed(float addedTime)
        {
            TimePassed += addedTime;
        }

        private IEnumerator TimerCoroutine()
        {
            while(TimePassed < TimerDuration)
            {
                if(IsTimerPaused) yield return null;
                
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

            _levelManagerRef.OnLevelCompletedEvent -= OnLevelCompletedEventHandlerMethod;
        }
    }
}
