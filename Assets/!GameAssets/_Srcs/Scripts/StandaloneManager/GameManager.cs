//----------------------------------------------------------------------
// Author   : "InsertYourNameHere"
// Created  : "YYYY/MM/DD"
//----------------------------------------------------------------------

using UnderworldCafe.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class summary
    /// </summary>
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        #region Dependency Injection
        [field: SerializeField] public InputManager InputManagerRef { get; private set; }
        [field: SerializeField] public SceneManager SceneManagerRef { get; private set; }
        [field: SerializeField] public PlayerGameResouces PlayerGameResoucesRef { get; private set; }
        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
    }
}
