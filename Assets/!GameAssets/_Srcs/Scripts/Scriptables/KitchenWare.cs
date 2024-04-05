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

        [SerializeField]int grade1Cost = 10;
        public int Grade1Cost { get { return grade1Cost; } private set { grade1Cost = value; } }
        [SerializeField] int grade2Cost = 15;
        public int Grade2Cost { get { return grade2Cost; } private set { grade2Cost = value; } }
        [SerializeField] int grade3Cost = 20;
        public int Grade3Cost { get { return grade3Cost; } private set { grade3Cost = value; } }

    }
}
