//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/01
//----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for animating timer for Customer's Order and Cooking Processes
    /// </summary>
    public class Timer : MonoBehaviour
    {
        // [SerializeField] Slider timerSlider;
        // [SerializeField] float timerDuration = 10f;

        // float timePassed;

        // [HideInInspector] public bool startTimer = false;
        // public float TimePassed => timePassed;

        
        // private void Start()
        // {
        //    timePassed = 0;
        //     if (gameObject.CompareTag("Customer")) //if object is customer
        //     {
        //         StartTimer();
        //     }
            
        // }

        // Update is called once per frame
        // void Update()
        // {
        //     if(startTimer)
        //     {
               
                
        //         UpdateTimer();
        //     }
            
        // }

        // private void ResetTimer()
        // {
        //     TimePassed = 0;
        //     TimerDuration = 0;
        // }

        // public void StartTimer() //start timer via function/button
        // {
        //    startTimer = true;
        //    ToggleTimerActivation(true);
        //    _timerCoroutine = StartCoroutine(UpdateTimer());
        // }

        // private void ToggleTimerActivation(bool toActive)
        // {
        //     timerSlider.gameObject.SetActive(toActive);
        // }

        // void UpdateTimer()
        // {
        //     if (!(UIManager.isPaused))
        //     {
        //         timePassed += Time.deltaTime;

        //         if (timePassed >= timerDuration) //timer reached end
        //         {
        //             timePassed = timerDuration;
        //             ToggleTimerActivation(false); //deactivate timer
        //             startTimer = false;

        //             if (!gameObject.CompareTag("Customer")) //if object is not customer
        //             {
        //                 ResetTimer();
        //             }
        //         }
        //         float timeNormalized = Mathf.Clamp01(timePassed / timerDuration);
        //         timerSlider.value = timeNormalized;
        //     }
            
        // }

        [SerializeField] private Slider timerSlider;

        public void ToggleTimerVisual(bool toActive)
        {
            timerSlider.gameObject.SetActive(toActive);
        }

        public void UpdateTimerSlider(float timePassed, float timerDuration)
        {
            float timeNormalized = Mathf.Clamp01(timePassed / timerDuration);
            timerSlider.value = timeNormalized;
        }
    }
}
