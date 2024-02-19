//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Kitchen Wares/Tools information
    /// </summary>
    [CreateAssetMenu(fileName = "Ware", menuName = "ScriptableObjects/KitchenWare", order = 3)]
    public class KitchenWare : ScriptableObject
    {
        public string wareName;
        public int grade = 0; //the grade of the tool/ware
        public Sprite wareImage;
    }
}
