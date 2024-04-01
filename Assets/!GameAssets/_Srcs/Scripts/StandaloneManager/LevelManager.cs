//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using System;
using UnityEngine;

using UnderworldCafe.GridSystem;
using UnderworldCafe.PathfindingSystem;
using UnderworldCafe.Player;
using UnderworldCafe.WaveSystem;

namespace UnderworldCafe
{
    /// <summary>
    /// Class should handle level information and become service locator for entire level lifecycle
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class LevelManager : DestroyOnLoadSingletonMonoBehaviour<LevelManager>
    {
        #region Dependencies
        [field: Header("DEPENDENCIES")]
        [field: SerializeField] public InputManager InputManager { get; private set; }
        [field: SerializeField] public GridManager GridManager { get; private set; }
        [field: SerializeField] public TimeManager TimeManager { get; private set; }
        [field: SerializeField] public PathRequestManager PathRequestManager { get; private set; }
        [field: SerializeField] public WaveManager WaveManager { get; private set; }
        [field: SerializeField] public PlayerController PlayerController { get; private set; }
        #endregion

        [Header("LEVEL SETTINGS")]
        [SerializeField] private float LevelTimeDuration;

        #region Events
        public event Action OnLevelCompletedEvent;
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            TimeManager.StartTimer(LevelTimeDuration);

            WaveManager.StartWaveSequence();
        }

        public void LevelIsCompleted()
        {
            OnLevelCompletedEvent?.Invoke();
        }

    }
}
