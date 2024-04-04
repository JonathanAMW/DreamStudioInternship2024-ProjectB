//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/01
//----------------------------------------------------------------------
//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Modified  : "2024/04/04
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe.CustomerSystem
{
    /// <summary>
    /// Class for animating timer for Customer's Order and Cooking Processes
    /// </summary>
    public class CustomerTimer : MonoBehaviour
    {
        [SerializeField] Slider timerSlider;

        public void UpdateTimerSlider(float timePassed, float timerDuration)
        {
            float timeNormalized = Mathf.Clamp01(timePassed / timerDuration);
            timerSlider.value = timeNormalized;
        }
    }
}
