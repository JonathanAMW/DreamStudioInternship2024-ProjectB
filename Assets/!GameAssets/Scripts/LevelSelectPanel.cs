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
        StageObject[] stageObjects;
        StageSelectManager _stageSelectManager;
        // Start is called before the first frame update
        void Start()
        {
            _stageSelectManager = FindAnyObjectByType<StageSelectManager>();
            stageObjects = _stageSelectManager.stageObjects;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateStageSelected(int selectedStageId)
        {
            _currentStageId=selectedStageId;
        }
    }
}
