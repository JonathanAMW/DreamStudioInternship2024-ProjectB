//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/22"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for managing game flow or store the global reference of the game
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        #region Dependencies
        [field: SerializeField] public SceneHandler SceneHandlerRef { get; private set; }
        [field: SerializeField] public PlayerGameResouces PlayerGameResoucesRef { get; private set; }
        #endregion

        

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
