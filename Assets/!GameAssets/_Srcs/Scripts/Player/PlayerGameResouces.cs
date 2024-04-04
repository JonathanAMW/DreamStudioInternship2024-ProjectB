//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class is responsible for managing player resources
    /// </summary>
    public class PlayerGameResouces : MonoBehaviour
    {
        private int _money = 100;
        public int Money => _money;
        
        
        public void AddPlayerMoney(int amount)
        {
            _money += amount;
        }
        public void ReducePlayerMoney(int amount)
        {
            _money -= amount;
        }
    }
}
