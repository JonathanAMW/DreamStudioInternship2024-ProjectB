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
            Debug.Log("Checking Player Resource Data");

            if(data.PlayerResourceDatas == null) return;

            Debug.Log("Loading Player Resource Data");

            _money = data.PlayerResourceDatas.Money;
            Debug.Log("Load money: "+ _money);
        }
        public void SaveData(GameData data)
        {
            Debug.Log("Saving Player Resource Data");
            if(data.PlayerResourceDatas == null)
            {
                data.PlayerResourceDatas = new PlayerResourceData();
            }

            data.PlayerResourceDatas.Money = _money;
            Debug.Log("Save money: " + _money);
        }
        #endregion
    }
}
