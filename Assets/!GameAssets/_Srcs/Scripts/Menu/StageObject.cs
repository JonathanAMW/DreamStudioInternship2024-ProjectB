//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using UnderworldCafe.DataPersistenceSystem;


namespace UnderworldCafe
{
    /// <summary>
    /// Blueprint of stage object
    /// </summary>
    public class StageObject : MonoBehaviour, IDataPersistence
    {
        [SerializeField] int stageId; 
        public int StageId { get { return stageId; } }

        [SerializeField] int totalLevels=1; //all levels in the stage, min.1
        public int TotalLevels { get { return totalLevels; } }

        public int unlockedLevels=0; //unlocked levels in the stage, min. 0-> level 1

        [SerializeField] int moneyRequired = 100; //all levels in the stage, min.1
        public int MoneyRequired { get { return moneyRequired; } }

        [SerializeField] Image _stageImg;
        public Image StageImage { get { return _stageImg; }}

        [SerializeField] Image _statusImg;
        public Image StatusImg { get { return _statusImg; } }


        public int starsEarnedInStage=0;
        public int starsRequired = 0;

        public bool isUnlockable = false; //enough stars to unlock
        public bool isOpened = false; //unlocked with money

        //public StageData stageData = null;

        StageSelectManager _stageSelectManager;
        LevelSelectPanel _levelSelectPanel;
        
        // Start is called before the first frame update
        void Awake()
        {
            _stageSelectManager = FindObjectOfType<StageSelectManager>();
            _levelSelectPanel = FindObjectOfType<LevelSelectPanel>();
        }

        private void Start()
        {
            //stageData = new StageData(unlockLevels: unlockedLevels, starsEarnedinStage: starsEarnedInStage, isUnlockable: isUnlockable, isOpened: isOpened );
            _stageSelectManager.UpdateOpenStageSprite(StageId);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ToggleStageButton(int stageId)
        {
            if (isOpened) //already unlocked
            {
                ShowLevelPanel();
                return;
            }
            _stageSelectManager.OpenStage(stageId);
        }

        void ShowLevelPanel()
        {
            StageSelectManager.currentStageId = StageId;
            _levelSelectPanel.UpdateStageSelected(StageSelectManager.currentStageId);
            _stageSelectManager.isLevelPanelActive = true;
            _stageSelectManager.levelSelectPanel.SetActive(true);
        }

        public int CalculateTotalStarsInStage(LevelSelectPanel.StageLevel[,] stageLevels)
        {
             starsEarnedInStage=0;
            for(int i = 0; i < TotalLevels;i++)
            {
                starsEarnedInStage += stageLevels[stageId, i].starsEarned;
            }
            return starsEarnedInStage;
        }


        #region DataPersistence
        public void LoadData(GameData data)
        {
            if(data.StageDatas.ContainsKey(StageId.ToString()))
            {
                unlockedLevels = data.StageDatas[StageId.ToString()].UnlockedLevels;
                starsEarnedInStage = data.StageDatas[StageId.ToString()].StarsEarnedInStage;
                isUnlockable = data.StageDatas[StageId.ToString()].IsUnlockable;
                isOpened = data.StageDatas[StageId.ToString()].IsOpened;
            }
        }

        public void SaveData(GameData data)
        {
            //If StageData of this Id doesn't exist, create one
            if(!data.StageDatas.ContainsKey(StageId.ToString()))
            {
                data.StageDatas.Add(StageId.ToString(), new StageData());
            }

            data.StageDatas[StageId.ToString()].UnlockedLevels = unlockedLevels;
            data.StageDatas[StageId.ToString()].StarsEarnedInStage = starsEarnedInStage;
            data.StageDatas[StageId.ToString()].IsUnlockable = isUnlockable;
            data.StageDatas[StageId.ToString()].IsOpened = isOpened;
        }
        #endregion
    }
}
