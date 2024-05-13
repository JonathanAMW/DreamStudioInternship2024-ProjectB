//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnderworldCafe.DataPersistenceSystem;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling Upgrade Menu functionalities
    /// </summary>
    public class UpgradeMenuManager : MonoBehaviour
    {
        UpgradeManager _upgradeManager;

        [SerializeField] GameObject nextPageButton;
        [SerializeField] GameObject prevPageButton;

        [SerializeField] TextMeshProUGUI playerMoneyText;
        public int PlayerMoney { get; private set; }

        PlayerGameResouces playerResource;
        public PlayerGameResouces PlayerResource { get { return playerResource; } private set { playerResource = value; } }

        private void Awake()
        {
            _upgradeManager = FindObjectOfType<UpgradeManager>();
           
        }

        private void Start()
        {
            
            ActivatePageButtons();
            UpdatePlayerMoney();
        }
        public void ToStageSelect()
        {
            SceneManager.LoadScene("StageSelect");
        }

        public void OpenOptions()
        {
            Debug.Log("Open options");
        }

        public void NextUpgradePage()
        {
            _upgradeManager.currentIndexNumber++;
            _upgradeManager.UpdateWareAppearance();

            ActivatePageButtons();
        }

        public void PrevUpgradePage()
        {
            _upgradeManager.currentIndexNumber--;
            _upgradeManager.UpdateWareAppearance();

            ActivatePageButtons();
          
        }

        void ActivatePageButtons()
        {
           
            if (_upgradeManager.currentIndexNumber >= _upgradeManager.kitchenWaresIndexCollection.Count - 1) //on last page
            {
                nextPageButton.SetActive(false);
            }
            else
            {
                nextPageButton.SetActive(true);
            }

            if (_upgradeManager.currentIndexNumber <= 0) //on first page
            {
                prevPageButton.SetActive(false);
            }
            else
            {
                prevPageButton.SetActive(true);
            }
        }

        public void UpdatePlayerMoney() //updates player's money UI
        {
            playerResource = GameManager.Instance.PlayerGameResouces;
            PlayerMoney = playerResource.Money;
            playerMoneyText.text = PlayerMoney.ToString();
        }

        #region DataPersistence
        public void LoadData(GameData data)
        {
            Debug.Log("Checking Player Resource Data");

            if (data.PlayerResourceDatas == null) return;

            Debug.Log("Loading Player Resource Data");

            PlayerMoney = data.PlayerResourceDatas.Money;
            Debug.Log("Load money: " + PlayerMoney);
        }
        public void SaveData(GameData data)
        {
            Debug.Log("Saving Player Resource Data");
            if (data.PlayerResourceDatas == null)
            {
                data.PlayerResourceDatas = new PlayerResourceData();
            }

            data.PlayerResourceDatas.Money = PlayerMoney;
            Debug.Log("Save money: " + PlayerMoney);
        }
        #endregion
    }
}
