//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/11"
//----------------------------------------------------------------------

namespace UnderworldCafe.DataPersistenceSystem
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