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
    /// controls functionalities of stageselect scene
    /// </summary>
    public class StageSelectManager : MonoBehaviour
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

        PlayerGameResouces playerResource= GameManager.Instance.PlayerGameResouces;

        public int totalStarsEarned = 0;

        private void Awake()
        {
            UpdatePlayerMoney();
            UpdateUnlockableStages();
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

        public void UpdateUnlockStage(int stageId)
        {

                stageObjects[stageId].StatusImg.GetComponent<Image>().sprite = lockedStageSprite;
                if (totalStarsEarned >= stageObjects[stageId].starsRequired && playerMoney >= stageObjects[stageId].MoneyRequired)
                {
                    
                    stageObjects[stageId].StatusImg.GetComponent<Image>().sprite = unlockedStageSprite;

                    if (stageObjects[stageId].isUnlockable)
                    {
                        playerResource.ReducePlayerMoney(stageObjects[stageId].MoneyRequired);
                        UpdatePlayerMoney();
                        stageObjects[stageId].isOpened = true;
                    }
                   
                }
            
            
        }

        public void UpdateUnlockableStages()
        {
            for(int i = 0; i < stageObjects.Length; i++)
            {
                stageObjects[i].isUnlockable = false;
                stageObjects[i].GetComponentInChildren<Button>().interactable = false;
                if (totalStarsEarned >= stageObjects[i].starsRequired)
                {
                    stageObjects[i].isUnlockable = true;
                    stageObjects[i].GetComponentInChildren<Button>().interactable = true;
                }
            }
        }
            public void UpdatePlayerMoney() //updates player's money UI
        {
            GameManager gameManager=FindObjectOfType<GameManager>();

            playerMoney = gameManager.PlayerGameResouces.Money;

            playerMoneyText.text = playerMoney.ToString();
        }
    }
}
