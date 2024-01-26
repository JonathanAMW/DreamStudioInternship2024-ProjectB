//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;


namespace UnderworldCafe
{
    /// <summary>
    /// Blueprint of stage object
    /// </summary>
    public class StageObject : MonoBehaviour

    {
        
        [SerializeField] int stageId; 
        public int StageId { get { return stageId; } }

        [SerializeField] int totalLevels=1; //all levels in the stage, min.1
        public int TotalLevels { get { return totalLevels; } }

        public int unlockedLevels=0; //unlocked levels in the stage, min. 0-> level 1

        [SerializeField] Image _stageImg;
        public Image StageImage { get { return _stageImg; }}


        StageSelectManager _stageSelectManager;
        LevelSelectPanel _levelSelectPanel;
        
        // Start is called before the first frame update
        void Awake()
        {
            _stageSelectManager = FindObjectOfType<StageSelectManager>();
            _levelSelectPanel = FindObjectOfType<LevelSelectPanel>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ShowLevelPanel()
        {
            StageSelectManager.currentStageId = StageId;
            _levelSelectPanel.UpdateStageSelected(StageSelectManager.currentStageId);
            _stageSelectManager.isLevelPanelActive = true;
            _stageSelectManager.levelSelectPanel.SetActive(true);
        }

       
    }
}
