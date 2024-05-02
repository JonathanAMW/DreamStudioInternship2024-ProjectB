//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnderworldCafe.DataPersistenceSystem;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class is responsible for managing player resources
    /// </summary>
    public class PlayerGameResouces : MonoBehaviour, IDataPersistence
    {
        private int _money;
        public int Money => _money;


        public void AddMoney(int amount)
        {
            _money += amount;
        }

        public void ReduceMoney(int amount)
        {
            _money -= amount;
        }

        #region DataPersistence
        public void LoadData(GameData data)
        {
            if(data.PlayerResourceDatas == null) return;

            _money = data.PlayerResourceDatas.Money;
        }
        public void SaveData(GameData data)
        {
            if(data.PlayerResourceDatas == null)
            {
                data.PlayerResourceDatas = new PlayerResourceData();
            }

            data.PlayerResourceDatas.Money = _money;
        }
        #endregion
    }
}
