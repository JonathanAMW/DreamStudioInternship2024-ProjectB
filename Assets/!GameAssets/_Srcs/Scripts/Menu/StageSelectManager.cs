//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using TMPro;
using UnderworldCafe.DataPersistenceSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// controls functionalities of stageselect scene
    /// </summary>
    public class StageSelectManager : MonoBehaviour, IDataPersistence
    {
        public StageObject[] stageObjects; //all stages in game
        public LevelObject[] levelObjects; //all levels in game
        public Sprite lockedStageSprite;
        public Sprite unlockedStageSprite;
        [HideInInspector] public bool isLevelPanelActive = false;
        public GameObject levelSelectPanel;
        public static int currentStageId = 0;

        [SerializeField] TextMeshProUGUI playerMoneyText;
        int playerMoney;
        PlayerGameResouces playerResource;

        public int totalStarsEarned = 0;

        private void Awake()
        {
            
        }

        private void Start()
        {
            playerResource = GameManager.Instance.PlayerGameResouces;
            UpdatePlayerMoney();

            
            UpdateUnlockableStages();

            for(int i = 0; i < stageObjects.Length; i++)
            {
                UpdateOpenStageSprite(i);
            }
            Debug.Log("Total stars: " + totalStarsEarned);
        }

        public void AssignTotalStarsEarned()
        {
            totalStarsEarned = FindObjectOfType<LevelSelectPanel>().totalStars;
        }
      
        public void GoBack()
        {
            if (isLevelPanelActive) //closes select level panel
            {
                isLevelPanelActive = false;
                levelSelectPanel.SetActive(false);
            }
            else //go to main menu
            {
                ToMainMenu();
            }
        }
        void ToMainMenu()
        {
            SceneManager.LoadScene(0); //load main menu scene
        }

        public void ToUpgradeMenu()
        {
            SceneManager.LoadScene("UpgradeMenu");
        }

        public void UpdateStar(LevelSelectPanel.StageLevel stageLevel, int stars)// updates the stars earned in the stage level, stars min. 1 to be considered completed
        {
            stageLevel.starsEarned= stars;

            stageLevel.isCompleted = false;
            if (stageLevel.starsEarned>0)
            {
                stageLevel.isCompleted = true;
            }

            
        }

        public void UpdateOpenStageSprite(int stageId)
        {
            
                stageObjects[stageId].StatusImg.GetComponent<Image>().sprite = lockedStageSprite;
                if (stageObjects[stageId].isOpened)
                {
                    
                    stageObjects[stageId].StatusImg.GetComponent<Image>().sprite = unlockedStageSprite;
                Debug.Log("Open stage " + stageId);
                }
            
            
        }

        public void OpenStage(int stageId)
        {
            
            if (stageObjects[stageId].isUnlockable && totalStarsEarned >= stageObjects[stageId].starsRequired && playerMoney >= stageObjects[stageId].MoneyRequired)
            {
                playerResource.ReduceMoney(stageObjects[stageId].MoneyRequired);
                UpdatePlayerMoney();
                stageObjects[stageId].isOpened = true;
            }
            UpdateOpenStageSprite(stageId);
        }

        public void UpdateUnlockableStages()
        {
            for(int i = 0; i < stageObjects.Length; i++)
            {
                stageObjects[i].isUnlockable = false;
                stageObjects[i].GetComponentInChildren<Button>().interactable = false;
                if (totalStarsEarned >= stageObjects[i].starsRequired || stageObjects[i].isUnlockable)
                {
                    stageObjects[i].isUnlockable = true;
                    stageObjects[i].GetComponentInChildren<Button>().interactable = true;
                }
            }
        }
        public void UpdatePlayerMoney() //updates player's money UI
        {
            playerMoney = playerResource.Money;
            playerMoneyText.text = playerMoney.ToString();
        }

        #region DataPersistence
        public void LoadData(GameData data)
        {
            Debug.Log("Checking Stage Data");

            if (data.StageDatas == null) return;

            Debug.Log("Loading Stage Data");

            /*for(int i = 0; i < stageObjects.Length; i++)
            {
                stageObjects[i].stageData = data.StageDatas[i.ToString()];
            }*/
        }
        public void SaveData(GameData data)
        {
            Debug.Log("Saving Stage Data");
            if (data.StageDatas == null)
            {
                data.StageDatas = new SerializableDictionary<string, StageData>();
            }

           /* for (int i = 0; i < stageObjects.Length; i++)
            {
                data.StageDatas[i.ToString()] = stageObjects[i].stageData;
            }*/

        }
        #endregion
    }
}
