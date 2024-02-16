using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] Sprite _goldStar;
        [SerializeField] Sprite _emptyStar;

        [SerializeField] Image[] _imgStars;
        [SerializeField] ScoreSlider _scoreSlider;

        public void UpdateStarSprites()
        {
            float timeNormalized = _scoreSlider.timeNormalized;
            float star3Percent = _scoreSlider.star3Percent;
            float star2Percent = _scoreSlider.star2Percent;
            float star1Percent = _scoreSlider.star1Percent;
            for(int i=0;i<_imgStars.Length;i++)
            {
                _imgStars[i].sprite=_goldStar;
            }
            if (timeNormalized <= star1Percent)
            {
                _imgStars[0].sprite = _emptyStar;
                _imgStars[1].sprite = _emptyStar;
                _imgStars[2].sprite = _emptyStar;
            }
            else if (timeNormalized <= star2Percent)
            {
                _imgStars[0].sprite = _emptyStar;
                _imgStars[1].sprite = _emptyStar;
            }
            else if (timeNormalized <= star3Percent)
            {
                _imgStars[2].sprite = _emptyStar;
            }

        }
        public void DoubleRewards()
        {
            Debug.Log("Loading Ads...");
        }
    }
}
