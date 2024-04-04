//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        int playerMoney;

        PlayerGameResouces playerResource = GameManager.Instance.PlayerGameResouces;

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
            playerMoney = playerResource.Money;
            playerMoneyText.text = playerMoney.ToString();
        }
    }
}
