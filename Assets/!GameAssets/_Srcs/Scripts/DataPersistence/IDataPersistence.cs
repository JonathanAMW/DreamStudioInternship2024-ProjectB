//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/11"
//----------------------------------------------------------------------

using UnderworldCafe.DataPersistenceSystem.GameDatas;

namespace UnderworldCafe.DataPersistenceSystem.Interfaces
{

    /// <summary>
    /// Interface for defining contract to implement load and save data behaviour in class
    /// </summary>
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(GameData data);
    }
}