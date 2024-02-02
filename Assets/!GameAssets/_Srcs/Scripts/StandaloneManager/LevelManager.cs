//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnderworldCafe.GridSystem;
using UnderworldCafe.Player;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class should handle level information and become service locator for entire level lifecycle
    /// </summary>
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        public GridManager GridManager { get; private set; }
        public PlayerController PlayerController { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            GridManager = FindObjectOfType<GridManager>();
            PlayerController = FindObjectOfType<PlayerController>();
        }

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
