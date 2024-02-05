//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;


namespace UnderworldCafe
{
    /// <summary>
    /// class for controlling the score slider for gameplay
    /// </summary>
    public class ScoreSlider : MonoBehaviour
    {
        [SerializeField] Slider scoreSlider;
        [SerializeField] float levelTimeDuration = 60f;

        [SerializeField] Image[] scoreStarImages = new Image[3];

        [SerializeField] const float star3Percent=0.8f;
        [SerializeField] const float star2Percent=0.5f;
        [SerializeField] const float star1Percent=0.2f;

        float timePassed;
        private void Start()
        {
            timePassed = levelTimeDuration;
        }

        // Update is called once per frame
        void Update()
        {
            timePassed -= Time.deltaTime;

            if (timePassed <= 0) //timer reached end
            {
                timePassed = 0;
            }
            float timeNormalized = Mathf.Clamp01(timePassed / levelTimeDuration);
            scoreSlider.value = timeNormalized;

            if (timeNormalized <= star1Percent)
            {
                scoreStarImages[0].color = Color.black;
            }
            else if (timeNormalized <= star2Percent)
            {
                scoreStarImages[1].color = Color.black;
            }
            else if (timeNormalized <= star3Percent)
            {
                scoreStarImages[2].color = Color.black;
            }
        }
    }
}
