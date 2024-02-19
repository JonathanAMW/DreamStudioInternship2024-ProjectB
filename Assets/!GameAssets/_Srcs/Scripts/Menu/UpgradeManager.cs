//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Upgrade related contents
    /// </summary>
    public class UpgradeManager : MonoBehaviour
    {
        public string wareName1;
        public GameObject[] grades1=new GameObject[3];
        public Image wareImage1;
        public string wareName2;
        public GameObject[] grades2 = new GameObject[3];
        public Image wareImage2;
        public string wareName3;
        public GameObject[] grades3 = new GameObject[3];
        public Image wareImage3;
        public KitchenWare[] kitchenWares; //lists of kitchen wares
    }
}
