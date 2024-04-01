//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/11"
//----------------------------------------------------------------------

using System.Collections.Generic;

namespace UnderworldCafe.DataPersistenceSystem
{
    /// <summary>
    /// Class for defining game data that will be saveable and loadable
    /// Try to save only necessary information! 
    /// Ex: SAVE variable that will change class behaviour and not the behaviour itself 
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        public long lastUpdated;

        public PlayerResourceDataStruct PlayerResourceDatas;
        public Dictionary<string, LevelData> LevelDatas;
        

        // The values defined in this constructor will be the default values the game starts with when there's no data to load
        public GameData()
        {
            PlayerResourceDatas = new PlayerResourceDataStruct(0);
            LevelDatas = new Dictionary<string, LevelData>();
        }
    }
    
    /// <summary>
    /// Struct for defining level data that will be saveable and loadable
    /// </summary>
    public struct LevelData
    {
        public string LevelName;
        public Dictionary<int, bool> LevelStars;
        public Dictionary<string, int> LevelUtensilsTier; 

        public LevelData(string levelName, Dictionary<int, bool> levelStars, Dictionary<string, int> levelUtensilsTier)
        {
            LevelName = levelName;
            LevelStars = levelStars;
            LevelUtensilsTier = levelUtensilsTier;
        }
    }


    public struct PlayerResourceDataStruct
    {
        public int Money;
        public PlayerResourceDataStruct(int money)
        {
            Money = money;
        }
    }
}