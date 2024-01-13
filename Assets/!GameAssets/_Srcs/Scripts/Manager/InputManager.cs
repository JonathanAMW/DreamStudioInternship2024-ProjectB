//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/09"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;


namespace UnderworldCafe.InputSystem
{
    /// <summary>
    /// This class will be used to handle input events from player
    /// </summary>
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        private GameInputAction _gameInputAction;


        protected override void Awake()
        {
            base.Awake();

            _gameInputAction = new GameInputAction();
        }

        private void Start()
        {
            
        }
    }
}
