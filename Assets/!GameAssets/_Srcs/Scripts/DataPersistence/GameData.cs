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
        public long LastUpdated;

        public PlayerResourceData PlayerResourceDatas;
        public Dictionary<string, LevelData> LevelDatas;
        

        // The values defined in this constructor will be the default values the game starts with when there's no data to load
        public GameData()
        {
            PlayerResourceDatas = new PlayerResourceData(0);
            LevelDatas = new Dictionary<string, LevelData>();
        }
    }
    
    /// <summary>
    /// Struct for defining level data that will be saveable and loadable
    /// </summary>
    public class LevelData
    {
        /// <summary>
        /// The Id of the level
        /// </summary>
        public string LevelId;

        /// <summary>
        /// Dictionary representing the stars aquired in each level, 
        /// where the key is an integer for index of each stars and the value is a bool for checking if that index's star has been aquired.
        /// </summary>
        public Dictionary<int, bool> LevelStars;

        /// <summary>
        /// Dictionary representing the tier of each utensil, where the key is a string reference ID and the value is an integer representing the tier.
        /// </summary>
        public Dictionary<string, int> LevelUtensilsTier; 

        public LevelData(string levelId, Dictionary<int, bool> levelStars, Dictionary<string, int> levelUtensilsTier)
        {
            LevelId = levelId;
            LevelStars = levelStars;
            LevelUtensilsTier = levelUtensilsTier;
        }
    }


    public class PlayerResourceData
    {
        public int Money;

        public PlayerResourceData(int money)
        {
            Money = money;
        }
    }
}