//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/01
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for animating timer for Customer's Order and Cooking Processes
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [SerializeField] Slider timerSlider;
        [SerializeField] float timerDuration = 10f;

        float timePassed;

        [HideInInspector] public bool startTimer = false;
        public float TimePassed => timePassed;
        private void Start()
        {
           timePassed = 0;
            if (gameObject.CompareTag("Customer")) //if object is customer
            {
                StartTimer();
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            if(startTimer)
            {
               
                
                UpdateTimer();
            }
            
        }

        void ResetTimer()
        {
            timePassed = 0;
        }

        public void StartTimer() //start timer via function/button
        {
           startTimer = true;
           ToggleTimerActivation(true);
        }
        void ToggleTimerActivation(bool toActive)
        {
            timerSlider.gameObject.SetActive(toActive);
        }
        void UpdateTimer()
        {
            
            if (!(UIManager.isPaused || UIManager.isRecipeOpen))
            {
                timePassed += Time.deltaTime;

                if (timePassed >= timerDuration) //timer reached end
                {
                    timePassed = timerDuration;
                    ToggleTimerActivation(false); //deactivate timer
                    startTimer = false;

                    if (!gameObject.CompareTag("Customer")) //if object is not customer
                    {
                        ResetTimer();
                    }
                }
                float timeNormalized = Mathf.Clamp01(timePassed / timerDuration);
                timerSlider.value = timeNormalized;
            }
            
        }


    }
}
