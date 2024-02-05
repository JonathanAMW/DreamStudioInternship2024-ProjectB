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
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject recipePanel;

        static public bool isPaused = false;
        static public bool isRecipeOpen = false;
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

        public void BackToStageSelect()
        {
            SceneManager.LoadScene("StageSelect"); //back to stage select scene
        }
    }
}
