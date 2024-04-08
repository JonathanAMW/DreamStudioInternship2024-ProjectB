//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using Unity.VisualScripting;
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
        [SerializeField] public float levelTimeDuration = 60f;

        [SerializeField] Image[] scoreStarImages = new Image[3];

        [SerializeField] public float star3Percent=0.8f;
        [SerializeField] public float star2Percent=0.5f;
        [SerializeField] public float star1Percent=0.2f;

        [HideInInspector] float timePassed;
        [HideInInspector] public float timeNormalized;
        [InspectorRange(0f,20f)]public float smoothness;

        LevelManager levelManager;
        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
        private void Start()
        {
            levelTimeDuration = levelManager.ReturnLevelDuration();
            timePassed = levelTimeDuration;
            FixStarsPlacement();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(!(UIManager.isPaused || UIManager.isResultOpen))
            {
                timePassed -= Time.deltaTime;

                if (timePassed <= 0) //timer reached end
                {
                    timePassed = 0;
                }
                timeNormalized = Mathf.Clamp01(timePassed / levelTimeDuration);
                scoreSlider.value = Mathf.Lerp(scoreSlider.value, timeNormalized, Time.deltaTime * smoothness);

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

        void FixStarsPlacement()
        {
            float sliderWidth = scoreSlider.gameObject.GetComponent<RectTransform>().rect.size.x;
            
            scoreStarImages[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(star1Percent * sliderWidth, scoreStarImages[0].GetComponent<RectTransform>().anchoredPosition.y);
            scoreStarImages[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(star2Percent * sliderWidth, scoreStarImages[1].GetComponent<RectTransform>().anchoredPosition.y);
            scoreStarImages[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(star3Percent * sliderWidth, scoreStarImages[2].GetComponent<RectTransform>().anchoredPosition.y);
        }
    }
}
