//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Controls functionalities of level select panel
    /// </summary>
    public class LevelSelectPanel : MonoBehaviour
    {
        int _currentStageId;
        StageLevel[,] stageLevels;
        StageLevel currentStageLevel;
        StageSelectManager _stageSelectManager;
        // Start is called before the first frame update

        [SerializeField] Sprite earnedStarImg;
        [SerializeField] Sprite nullStarImg;

        [SerializeField] TextMeshProUGUI earnedStarsInStageText;
        public class StageLevel
        {
            public int stageId;
            public int levelId;
            public int starsEarned=0;
            public bool isCompleted;
        }
        void Awake()
        {
            _stageSelectManager = FindAnyObjectByType<StageSelectManager>();
           
        }
        private void Start()
        {
            if(_stageSelectManager == null)
            {
                Debug.Log("Stage select manager null");
            }

            int stageLength = _stageSelectManager.stageObjects.Length;
            int levelLength = _stageSelectManager.levelObjects.Length;

            stageLevels = new StageLevel[stageLength, levelLength];
            for (int i = 0; i < stageLength; i++)//assign stagelevels
            {
                for(int j = 0; j < _stageSelectManager.stageObjects[i].TotalLevels; j++)
                {

                    stageLevels[i,j]=new StageLevel();
                    stageLevels[i, j].stageId = _stageSelectManager.stageObjects[i].StageId;
                    stageLevels[i, j].levelId = _stageSelectManager.levelObjects[j].LevelId;
                }
            }

            CalculateTotalStars();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateStageSelected(int selectedStageId)
        {
            _currentStageId=selectedStageId;
            for(int i = 0; i < _stageSelectManager.levelObjects.Length; i++) //display all levels for that stage
            {
                _stageSelectManager.levelObjects[i].gameObject.SetActive(false);
                if (i < _stageSelectManager.stageObjects[selectedStageId].TotalLevels)
                {
                    _stageSelectManager.levelObjects[i].gameObject.SetActive(true);
                }
               
            }
            earnedStarsInStageText.text = string.Format("{0}/{1}", _stageSelectManager.stageObjects[selectedStageId].starsEarnedInStage, _stageSelectManager.stageObjects[selectedStageId].TotalLevels*3); //change text of stars earned in that stage
            UpdateUnlockedLevel();
            UpdateStarSprite();
        }

        public void UpdateUnlockedLevel()
        {
            _stageSelectManager.stageObjects[_currentStageId].unlockedLevels=0;
            for (int j = 0; j < _stageSelectManager.stageObjects[_currentStageId].TotalLevels; j++)
            {
                if (stageLevels[_currentStageId,j].isCompleted)
                {
                    _stageSelectManager.stageObjects[_currentStageId].unlockedLevels++;
                }
            }

            for(int i= 0; i < _stageSelectManager.levelObjects.Length; i++)//show all unlocked levels
            {
                _stageSelectManager.levelObjects[i].GetComponent<Button>().interactable=false;
                if(i <= _stageSelectManager.stageObjects[_currentStageId].unlockedLevels)
                {
                    _stageSelectManager.levelObjects[i].GetComponent<Button>().interactable = true;
                }
            }
        }

        public void SelectLevel(int levelId) //upon pressing level
        {


                currentStageLevel = stageLevels[_currentStageId, levelId];
                
                int starEarned = currentStageLevel.starsEarned;
                starEarned++;
                _stageSelectManager.UpdateStar(currentStageLevel, starEarned);
                UpdateStarSprite();
            UpdateUnlockedLevel();
            CalculateTotalStars();
            //SceneManager.LoadScene(2); //loads gameplay
        }

        void CalculateTotalStars()
        {
            int totalStars = 0;
            for(int i=0 ; i<_stageSelectManager.stageObjects.Length; i++)
            {
                totalStars += _stageSelectManager.stageObjects[i].CalculateTotalStarsInStage(stageLevels);
            }
            _stageSelectManager.totalStarsEarned = totalStars;
            _stageSelectManager.UpdateUnlockStage(stageLevels);
            earnedStarsInStageText.text = string.Format("{0}/{1}", _stageSelectManager.stageObjects[_currentStageId].starsEarnedInStage.ToString(), _stageSelectManager.stageObjects[_currentStageId].TotalLevels * 3); //change text of stars earned in that stage
        }

        public void UpdateStarSprite() //updates the sprite on the level objects based on the current stage
        {
            for(int i = 0; i < _stageSelectManager.stageObjects[_currentStageId].TotalLevels;i++) //loops through each level on that stage
            {
                int starsEarned = stageLevels[_currentStageId, i].starsEarned ;
                
                for (int j = 0; j < 3;j++)
                {
                    _stageSelectManager.levelObjects[i].levelStars[j].GetComponent<Image>().sprite = nullStarImg;
                    if (j < starsEarned)
                    {
                        _stageSelectManager.levelObjects[i].levelStars[j].GetComponent<Image>().sprite = earnedStarImg;
                    }
                }
            }
        }


    }
}
