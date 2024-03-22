//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/01
//----------------------------------------------------------------------

using System;

using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for animating timer for Customer's Order
    /// </summary>
    public class Timer : MonoBehaviour
    {
        #region Events
        public event Action OnTimerEndedEvent;
        #endregion
        
        
        [SerializeField] Slider timerSlider;
        [SerializeField] float timerDuration = 10f;
        float timePassed;

        public float TimePassed => timePassed;
        
        
        private void Start()
        {
            timePassed = 0;
        }

        // Update is called once per frame
        private void Update()
        {
            if(!(UIManager.isPaused || UIManager.isRecipeOpen))
            {
                timePassed += Time.deltaTime;

                if (timePassed >= timerDuration) //timer reached end
                {
                    timePassed = timerDuration;
                    OnTimerEndedEvent?.Invoke();
                }
                float timeNormalized = Mathf.Clamp01(timePassed / timerDuration);
                timerSlider.value = timeNormalized;
            }
        }

        //For time penalty mechanic
        public void AddTimePassed(float addedTime)
        {
            timePassed += addedTime;
        }



    }
}
