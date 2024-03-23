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
        #region Events
        public event Action OnTimerEndedEvent;
        #endregion
        

        public bool IsTimerPaused { get; private set; }
        public float TimerDuration { get; private set; }  
        public float TimePassed { get; private set; }
        

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

        private IEnumerator TimerCoroutine()
        {
            while(TimePassed < TimerDuration)
            {
                if(IsTimerPaused) yield return null;
                
                TimePassed += Time.deltaTime;
                Debug.Log(TimePassed);
                yield return null;
            }

            TimePassed = TimerDuration;
            OnTimerEndedEvent?.Invoke();
            Debug.Log("Timer ended");
        }

        //For time penalty mechanic
        public void AddTimePassed(float addedTime)
        {
            TimePassed += addedTime;
        }
    }
}
