//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using TMPro;
using UnderworldCafe.DataPersistenceSystem;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Controls functionalities of level select panel
    /// </summary>
    public class LevelSelectPanel : MonoBehaviour, IDataPersistence
    {
        int _currentStageId;
        public int totalStars;//total stars earn in game
        StageLevel[,] stageLevels;
        StageLevel currentStageLevel;
        StageSelectManager _stageSelectManager;
        // Start is called before the first frame update

        [SerializeField] Sprite earnedStarImg;
        [SerializeField] Sprite nullStarImg;

        [SerializeField] TextMeshProUGUI earnedStarsInStageText;
        [SerializeField] GameObject totalStarGUI;
        
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
            int stageLength = _stageSelectManager.stageObjects.Length;
            int levelLength = _stageSelectManager.levelObjects.Length;

            stageLevels = new StageLevel[stageLength, levelLength];
            for (int i = 0; i < stageLength; i++)//assign stagelevels
            {
                for (int j = 0; j < _stageSelectManager.stageObjects[i].TotalLevels; j++)
                {

                    stageLevels[i, j] = new StageLevel();
                    stageLevels[i, j].stageId = _stageSelectManager.stageObjects[i].StageId;
                    stageLevels[i, j].levelId = _stageSelectManager.levelObjects[j].LevelId;
                }
            }
        }
        private void Start()
        {
            if(_stageSelectManager == null)
            {
                Debug.Log("Stage select manager null");
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
            LevelObject lastLevel = _stageSelectManager.levelObjects[_stageSelectManager.stageObjects[_currentStageId].TotalLevels];

            //position of total stars on stage box
            float xPos= gameObject.GetComponent<RectTransform>().rect.width-465; //the right position of the panel
            float yPos = lastLevel.GetComponent<RectTransform>().anchoredPosition.y+50; //position below the last level height
            totalStarGUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            earnedStarsInStageText.text = string.Format("{0}/{1}", _stageSelectManager.stageObjects[selectedStageId].starsEarnedInStage, _stageSelectManager.stageObjects[selectedStageId].TotalLevels*3); //change text of stars earned in that stage
            UpdateUnlockedLevel();
            UpdateStarSprite();
        }

        public void UpdateUnlockedLevel()
        {
            _stageSelectManager.stageObjects[_currentStageId].unlockedLevels=0;
            for (int j = 0; j < _stageSelectManager.stageObjects[_currentStageId].TotalLevels; j++) //unlocks levels on that stage
            {
                if (stageLevels[_currentStageId,j].isCompleted && j+1 < _stageSelectManager.stageObjects[_currentStageId].TotalLevels) //unlocks next level if current level is already completed                                                                                                                   //while make sure that the current level is not the last level
                {
                    _stageSelectManager.stageObjects[_currentStageId].unlockedLevels++;
                }
            }

            for(int i= 0; i < _stageSelectManager.stageObjects[_currentStageId].TotalLevels; i++)//show all unlocked levels
            {

                _stageSelectManager.levelObjects[i].GetComponent<Button>().interactable = false;
                _stageSelectManager.levelObjects[i].statusImg.SetActive(true);
                if (i <= _stageSelectManager.stageObjects[_currentStageId].unlockedLevels) //make unlocked levels interactable
                {
                    _stageSelectManager.levelObjects[i].GetComponent<Button>().interactable = true;
                    _stageSelectManager.levelObjects[i].statusImg.SetActive(false);
                }
               
            }
        }

        public void SelectLevel(int levelId) //upon pressing level
        {
            currentStageLevel = stageLevels[_currentStageId, levelId];
                
            int starEarned = currentStageLevel.starsEarned;
            if(starEarned < 3)
            {
                starEarned++;
            }
            
            _stageSelectManager.UpdateStar(currentStageLevel, starEarned);
            UpdateStarSprite();
            UpdateUnlockedLevel();
            CalculateTotalStars();
            //SceneManager.LoadScene(2); //loads gameplay
        }

        void CalculateTotalStars()
        {
            totalStars = 0;
            for(int i=0 ; i<_stageSelectManager.stageObjects.Length; i++)
            {
                totalStars += _stageSelectManager.stageObjects[i].CalculateTotalStarsInStage(stageLevels);
            }
            _stageSelectManager.totalStarsEarned = totalStars;
            _stageSelectManager.UpdateUnlockableStages();
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

        #region DataPersistence
        public void LoadData(GameData data)
        {
            for(int i = 0; i< _stageSelectManager.stageObjects.Length; i++)
            {
                if (data.StageDatas.ContainsKey(_stageSelectManager.stageObjects[i].StageId.ToString()))
                {
                    for (int j = 0; j < _stageSelectManager.stageObjects[i].TotalLevels; j++)
                    {
                        if (data.StageDatas[i.ToString()].LevelDatas.ContainsKey(stageLevels[i,j].levelId.ToString()))
                        {
                            stageLevels[i, j].starsEarned = data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].StarsEarnedInLevel;
                            //Debug.Log("Load Level " + i + "," + j + " has stars earned: " + data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].StarsEarnedInLevel);
                            stageLevels[i, j].isCompleted = data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].IsUnlocked; //completed means passed the level
                        }    
                    }
                        
                }
            }
           
        }

        public void SaveData(GameData data)
        {
            for (int i = 0; i < _stageSelectManager.stageObjects.Length; i++)
            {
                if (!data.StageDatas.ContainsKey(_stageSelectManager.stageObjects[i].StageId.ToString()))
                {
                    data.StageDatas.Add(_stageSelectManager.stageObjects[i].StageId.ToString(), new StageData());
                }

                if (data.StageDatas.ContainsKey(_stageSelectManager.stageObjects[i].StageId.ToString()))
                {
                    for (int j = 0; j < _stageSelectManager.stageObjects[i].TotalLevels; j++)
                    {
                        if (!(data.StageDatas[i.ToString()].LevelDatas.ContainsKey(stageLevels[i, j].levelId.ToString())))
                        {
                            //Debug.Log("Creating new level data");
                            data.StageDatas[i.ToString()].LevelDatas.Add(stageLevels[i, j].levelId.ToString(), new LevelData());
                        }
                        data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].StarsEarnedInLevel = stageLevels[i, j].starsEarned;
                        //Debug.Log("Saved Level "+ i + ","+ j + " hsa stars earned: " + data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].StarsEarnedInLevel);
                        data.StageDatas[i.ToString()].LevelDatas[stageLevels[i, j].levelId.ToString()].IsUnlocked = stageLevels[i, j].isCompleted;
                    }
                }
            }
        }

        #endregion

    }
}
