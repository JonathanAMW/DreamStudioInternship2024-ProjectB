//----------------------------------------------------------------------
// Author   : "InsertYourNameHere"
// Created  : "YYYY/MM/DD"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class summary
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        #region Dependency Injection
        // [field: SerializeField] public SceneHandler SceneHandlerRef { get; private set; }
        [field: SerializeField] public PlayerGameResouces PlayerGameResoucesRef { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
