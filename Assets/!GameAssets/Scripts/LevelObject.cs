//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/01/26"
//----------------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UnderworldCafe
{
    /// <summary>
    /// Blueprint of level object
    /// </summary>
    public class LevelObject : MonoBehaviour
    {
        [SerializeField] int levelId;
        public int LevelId { get { return levelId; } }

        [SerializeField] public Image[] levelStars= new Image[3];
        [SerializeField] TextMeshProUGUI textLevel;
        [SerializeField] GameObject statusImg;


        private void Start()
        {
            textLevel.text = "Level " + (levelId+1);
        }
    }
}
