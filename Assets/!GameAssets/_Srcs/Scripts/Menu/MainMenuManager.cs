//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/24"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling all functionalities of main menu
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
       public void ToSelectLevel()
       {
            SceneManager.LoadScene(1); //load stage select scene
       }
    }
}
