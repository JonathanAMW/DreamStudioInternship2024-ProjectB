//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/22"
//----------------------------------------------------------------------

using UnityEngine;
using UnderworldCafe.DataPersistenceSystem;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for managing game flow or store the global reference of the game
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        #region Dependencies
        [field: SerializeField] public DataPersistenceManager DataPersistenceManager { get; private set; }
        [field: SerializeField] public AudioManager AudioManager { get; private set; }
        [field: SerializeField] public SceneHandler SceneHandler { get; private set; }
        [field: SerializeField] public PlayerGameResouces PlayerGameResouces { get; private set; }
        #endregion

        public bool IsGamePaused { get; private set; } 
        
        protected override void Awake()
        {
            base.Awake();

            IsGamePaused = false;
        }

        public void PauseGame(bool isPaused)
        {
            IsGamePaused = isPaused;
            AudioListener.pause = isPaused;

            if(isPaused)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}
