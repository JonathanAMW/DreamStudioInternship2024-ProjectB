//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/11"
//----------------------------------------------------------------------

using System.Collections.Generic;

namespace UnderworldCafe.DataPersistenceSystem
{
    /// <summary>
    /// Class for defining game data that will be saved and loaded
    /// Try to save only necessary information! 
    /// Ex: SAVE variable that will change class behaviour and not the behaviour itself 
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        public long LastUpdated;

        public PlayerResourceData PlayerResourceDatas;

        /// <summary>
        /// Dictionary for storing all the game stage datas. 
        /// The key is a string for id of the stage, while the value is a class representing that stage data.
        /// </summary>
        public SerializableDictionary<string, StageData> StageDatas;

        /// <summary>
        /// Dictionary for storing all the game level datas. 
        /// The key is a string for id of the level, while the value is a class representing that level data.
        /// </summary>
        public SerializableDictionary<string, LevelData> LevelDatas;
        

        //Default Constructor
        // The values defined in this constructor will be the default values the game starts with when there's no data to load
        public GameData()
        {
            PlayerResourceDatas = new PlayerResourceData();
            StageDatas = new SerializableDictionary<string, StageData>();
            LevelDatas = new SerializableDictionary<string, LevelData>();
        }
    }
    

    /// <summary>
    /// Class for defining the stage data that will be saved and loaded
    /// </summary>
    public class StageData
    {
        public int UnlockedLevels;
        public int StarsEarnedInStage;
        public bool IsUnlockable;
        public bool IsOpened;


        //Default Constructor
        public StageData()
        {
            UnlockedLevels = 0;
            StarsEarnedInStage = 0;
            IsUnlockable = false;
            IsOpened = false;
        }
    }


    /// <summary>
    /// Class for defining the level data that will be saved and loaded
    /// </summary>
    public class LevelData
    {
        public bool IsUnlocked;
        public int LevelStarsEarned;

        /// <summary>
        /// Dictionary representing the tier of each utensil. 
        /// The key is a string reference ID, while the value is an integer representing the tier.
        /// </summary>
        public SerializableDictionary<string, int> LevelUtensilsTier;


        //Default Constructor
        public LevelData()
        {
            IsUnlocked = false;
            LevelStarsEarned = 0;
            LevelUtensilsTier = new SerializableDictionary<string, int>();
        }
    }


    public class PlayerResourceData
    {
        public int Money;


        //Default Constructor
        public PlayerResourceData()
        {
            Money = 0;
        }
    }
}