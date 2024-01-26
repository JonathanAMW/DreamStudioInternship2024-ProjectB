//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/25"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Controls functionalities of level select panel
    /// </summary>
    public class LevelSelectPanel : MonoBehaviour
    {
        int _currentStageId;
        StageLevel[,] stageLevels;
        StageSelectManager _stageSelectManager;
        // Start is called before the first frame update

        public class StageLevel
        {
            public int stageId;
            public int levelId;
            public int starsEarned;
            public bool isCompleted;
        }
        void Awake()
        {
            _stageSelectManager = FindAnyObjectByType<StageSelectManager>();
           
        }
        private void Start()
        {
            if(_stageSelectManager == null)
            {
                Debug.Log("Stage select manager null");
            }

            int stageLength = _stageSelectManager.stageObjects.Length;
            int levelLength = _stageSelectManager.levelObjects.Length;

            stageLevels = new StageLevel[stageLength, levelLength];
            for (int i = 0; i < stageLength; i++)
            {
                for(int j = 0; j < _stageSelectManager.stageObjects[i].TotalLevels; j++)
                {

                    stageLevels[i,j]=new StageLevel();
                    stageLevels[i, j].stageId = _stageSelectManager.stageObjects[i].StageId;
                    stageLevels[i, j].levelId = _stageSelectManager.levelObjects[j].LevelId;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateStageSelected(int selectedStageId)
        {
            _currentStageId=selectedStageId;
            for(int i = 0; i < _stageSelectManager.levelObjects.Length; i++) //display all levels for that stage
            {
                _stageSelectManager.levelObjects[i].gameObject.SetActive(false);
                if (i < _stageSelectManager.stageObjects[selectedStageId].TotalLevels)
                {
                    _stageSelectManager.levelObjects[i].gameObject.SetActive(true);
                }
               
            }
        }

        public void UpdateUnlockedLevel()
        {
            for(int i= 0; i < _stageSelectManager.levelObjects.Length; i++)
            {
                _stageSelectManager.levelObjects[i].gameObject.SetActive(false);
                if(i < _stageSelectManager.stageObjects[_currentStageId].unlockedLevels)
                {
                    _stageSelectManager.levelObjects[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
