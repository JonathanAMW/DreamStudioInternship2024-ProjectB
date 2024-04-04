//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/05"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling UI functionalities for game scenes
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Dependencies
        private LevelManager _levelManagerRef;
        #endregion

        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject recipePanel;
        [SerializeField] GameObject resultPanel;
        [SerializeField] GameObject settingsPanel;

        static public bool isPaused = false;
        static public bool isRecipeOpen = false;
        static public bool isResultOpen = false;
        static public bool isSettingsOpened=false;

        private void Awake()
        {
            _levelManagerRef = LevelManager.Instance;
        }
        private void OnEnable()
        {
            _levelManagerRef.OnLevelCompletedEvent += OnLevelCompletedEventHandlerMethod;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                ShowScore();
            }
        }

        public void PauseGame()
        {
            if(isPaused==false)
            {
                pausePanel.SetActive(true);
            }
            else //is already paused
            {
                pausePanel.SetActive(false);
            }
            isPaused = !isPaused;

        }

        public void ToggleRecipe()
        {
            if (isRecipeOpen == false)
            {
                recipePanel.SetActive(true);
            }
            else //is already paused
            {
                recipePanel.SetActive(false);
            }
            isRecipeOpen = !isRecipeOpen;

        }

        public void ToggleSettings()
        {

            if (isSettingsOpened == false)
            {
                settingsPanel.SetActive(true);
            }
            else //is already opened settings
            {
                settingsPanel.SetActive(false);
            }
            isSettingsOpened = !isSettingsOpened;

        }

        public void ShowScore()
        {
            isResultOpen = true;
            resultPanel.SetActive(true);
            resultPanel.GetComponent<ResultPanel>().UpdateStarSprites();
        }

        public void BackToStageSelect()
        {
            SceneManager.LoadScene("StageSelect"); //back to stage select scene
        }

        private void OnLevelCompletedEventHandlerMethod()
        {
            ShowScore();
        }

    }
}
