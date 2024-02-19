//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling Upgrade Menu functionalities
    /// </summary>
    public class UpgradeMenuManager : MonoBehaviour
    {
        public void ToStageSelect()
        {
            SceneManager.LoadScene("StageSelect");
        }

        public void OpenOptions()
        {
            Debug.Log("Open options");
        }
    }
}
