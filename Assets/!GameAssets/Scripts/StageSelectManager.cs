//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// controls functionalities of stageselect scene
    /// </summary>
    public class StageSelectManager : MonoBehaviour
    {
        public StageObject[] stageObjects; //all stages in game
        [HideInInspector]public bool isLevelPanelActive = false;
        public GameObject levelSelectPanel;
        public static int currentStageId = 0;

        public void GoBack()
        {
            if(isLevelPanelActive) //closes select level panel
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
    }
}
