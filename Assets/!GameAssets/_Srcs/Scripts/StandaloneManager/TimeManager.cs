//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/22"
//----------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        
        
        private float _timerDuration;
        private float _timePassed;


        public float TimerDuration =>_timerDuration;        
        public float TimePassed => _timePassed;
        

        public void StartTimer(float timerDuration)
        {
            _timePassed = 0;
            _timerDuration = timerDuration;

            StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            StopCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while(_timePassed < _timerDuration)
            {
                _timePassed += Time.deltaTime;

                yield return null;
            }

            _timePassed = _timerDuration;
            OnTimerEndedEvent?.Invoke();
        }

        //For time penalty mechanic
        public void AddTimePassed(float addedTime)
        {
            _timePassed += addedTime;
        }
    }
}
