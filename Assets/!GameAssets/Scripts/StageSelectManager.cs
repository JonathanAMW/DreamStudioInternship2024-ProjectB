//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

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
        [HideInInspector]public bool isLevelPanelActive = false;
        public GameObject levelSelectPanel;
        public static int currentStageId = 0;

        public int totalStarsEarned=0;


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

        public void UpdateStar(LevelSelectPanel.StageLevel stageLevel, int stars)// updates the stars earned in the stage level, stars min. 1 to be considered completed
        {
            stageLevel.starsEarned= stars;

            stageLevel.isCompleted = false;
            if (stageLevel.starsEarned>0)
            {
                stageLevel.isCompleted = true;
            }

            
        }

        public void UpdateUnlockStage(LevelSelectPanel.StageLevel[,] stageLevels)
        {
            for(int i = 0; i < stageObjects.Length-1; i++)
            {
                stageObjects[i + 1].GetComponentInChildren<Button>().interactable = false;
                if (totalStarsEarned >= stageObjects[i+1].starsRequired)
                {
                    stageObjects[i + 1].GetComponentInChildren<Button>().interactable = true;
                }
            }
            
        }
    }
}
