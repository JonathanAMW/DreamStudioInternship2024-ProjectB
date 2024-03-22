//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnderworldCafe.GridSystem;
using UnderworldCafe.Player;
using UnityEngine;


namespace UnderworldCafe.GameManageralSystem
{
    /// <summary>
    /// Class should handle level information and become service locator for entire level lifecycle
    /// </summary>
    public class LevelManager : DestroyOnLoadSingletonMonoBehaviour<LevelManager>
    {
        #region Dependency Injection
        public GridManager LevelGridManager { get; private set; }
        public PlayerController LevelPlayerController { get; private set; }
        public Timer LevelTimer { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            LevelGridManager = FindObjectOfType<GridManager>();
            LevelPlayerController = FindObjectOfType<PlayerController>();
            LevelTimer = FindObjectOfType<Timer>();
        }
    }
}
