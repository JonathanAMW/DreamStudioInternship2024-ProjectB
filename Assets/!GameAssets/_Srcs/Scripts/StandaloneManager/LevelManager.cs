//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnderworldCafe.GridSystem;
using UnderworldCafe.PathfindingSystem;
using UnderworldCafe.Player;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class should handle level information and become service locator for entire level lifecycle
    /// </summary>
    public class LevelManager : DestroyOnLoadSingletonMonoBehaviour<LevelManager>
    {
        #region Dependency Injection
        [field: SerializeField] public InputManager LevelInputManagerRef { get; private set; }
        [field: SerializeField] public GridManager LevelGridManagerRef { get; private set; }
        [field: SerializeField] public TimeManager LevelTimeManagerRef { get; private set; }
        [field: SerializeField] public PoolManager LevelPoolManagerRef { get; private set; }
        [field: SerializeField] public PathRequestManager LevelPathRequestManagerRef { get; private set; }
        [field: SerializeField] public PlayerController LevelPlayerControllerRef { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            
        }

    }
}
