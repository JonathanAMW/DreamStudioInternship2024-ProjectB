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

        [Header("Unlock Stage Confirmation Panel")]
        public GameObject unlockStageConfirmPanel;
        public TextMeshProUGUI desc;
        public TextMeshProUGUI successMessage;
        public Color32 moneyAmountColor; //yellow
        public Color32 successMessageColor; //green
        public Color32 unsuccessfulMessageColor; //red


        private void Awake()
        {
            
        }

        private void Start()
        {
           
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

        public void OpenStage()
        {
            successMessage.gameObject.SetActive(true);
            if (stageObjects[currentStageId].isUnlockable && totalStarsEarned >= stageObjects[currentStageId].starsRequired && playerMoney >= stageObjects[currentStageId].MoneyRequired && !stageObjects[currentStageId].isOpened)
            {
                successMessage.color = successMessageColor;
                successMessage.text = "Stage Opened";

                playerResource.ReduceMoney(stageObjects[currentStageId].MoneyRequired);
                UpdatePlayerMoney();
                stageObjects[currentStageId].isOpened = true;
            }
            else if (stageObjects[currentStageId].isUnlockable && totalStarsEarned >= stageObjects[currentStageId].starsRequired && playerMoney < stageObjects[currentStageId].MoneyRequired)//not enough money
            {
                successMessage.color = unsuccessfulMessageColor;
                successMessage.text = "Not enough coins to offer";
            }
            else if(stageObjects[currentStageId].isOpened) //already opened
            {
                successMessage.color = Color.white;
                successMessage.text = "Stage has already been unsealed";
            }
            UpdateOpenStageSprite(currentStageId);
        }

        public void ToggleConfirmationPanel(int stageId)
        {
            if (unlockStageConfirmPanel.activeSelf)//already opened panel
            {
                unlockStageConfirmPanel.SetActive(false);
                successMessage.gameObject.SetActive(false);
            }
            else
            {
                string hexMoneyAmountColor = ColorUtility.ToHtmlStringRGB(moneyAmountColor);

                currentStageId = stageId;
                unlockStageConfirmPanel.SetActive(true);
                successMessage.gameObject.SetActive(false);
                string coinsRequired = stageObjects[stageId].MoneyRequired.ToString();
                desc.text = $"Offer <color=#{hexMoneyAmountColor}><b>{coinsRequired}</b></color> coins to unseal this stage?";
            }

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
            playerResource = GameManager.Instance.PlayerGameResouces;
            playerMoney = playerResource.Money;
            playerMoneyText.text = playerMoney.ToString();
        }

        #region DataPersistence
        public void LoadData(GameData data)
        {
            Debug.Log("Checking Stage Data");

            if (data.StageDatas == null) return;

            Debug.Log("Loading Stage Data");       
        }
        public void SaveData(GameData data)
        {
            Debug.Log("Saving Stage Data");
            if (data.StageDatas == null)
            {
                data.StageDatas = new SerializableDictionary<string, StageData>();
            }
        }
        #endregion
    }
}
