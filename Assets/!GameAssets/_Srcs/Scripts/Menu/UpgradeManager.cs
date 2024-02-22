//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/19"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Upgrade related contents
    /// </summary>
    public class UpgradeManager : MonoBehaviour
    {
        public TextMeshProUGUI wareName1;
        public GameObject[] grades1=new GameObject[3];
        public Image wareImage1;
        public TextMeshProUGUI wareName2;
        public GameObject[] grades2 = new GameObject[3];
        public Image wareImage2;
        public TextMeshProUGUI wareName3;
        public GameObject[] grades3 = new GameObject[3];
        public Image wareImage3;
        public KitchenWare[] kitchenWares; //lists of kitchen wares

        [SerializeField] GameObject[] upgradeItemObjects= new GameObject[3];
        [SerializeField] Color activeGradeColor;
        [SerializeField] Color inactiveGradeColor;

        KitchenWare[] currentKitchenWares= new KitchenWare[3];

        public List<int[]> kitchenWaresIndexCollection= new List<int[]>();
        public int currentIndexNumber=0;
        int[] currentIndexCollection = new int[3];
        private void Awake()
        {
            //assign kitchewareindexcollection based on kitchenwares
            int j = 0;
            for (int i = 0; i < kitchenWares.Length; i++)
            {
                List<int> indexCollection=new List<int>();
                if(j<kitchenWares.Length)
                {
                    indexCollection.Add(j);
                    if (j + 1 < kitchenWares.Length)
                    {
                        indexCollection.Add(j+1);

                        if (j + 2 < kitchenWares.Length)
                        {
                            indexCollection.Add(j+2);
                        }
                    }

                    kitchenWaresIndexCollection.Add(indexCollection.ToArray());
                    j = j + 3;
                }


            }

            currentIndexNumber = 0;//currentKitchenWares is the first 3 wares


            UpdateWareAppearance();
        }

        public void UpdateWareAppearance()
        {
            currentIndexCollection = kitchenWaresIndexCollection[currentIndexNumber]; 
            for (int i = 0; i < currentKitchenWares.Length; i++) //assigns currentKitchenWares
            {

                if (i < kitchenWaresIndexCollection[currentIndexNumber].Length)
                {
                    currentKitchenWares[i] = kitchenWares[currentIndexCollection[i]];
                }
                else
                {
                    currentKitchenWares[i] = null;
                }
               
            }

            if (currentKitchenWares[0] !=null)
            {
                upgradeItemObjects[0].SetActive(true);
                wareName1.text = currentKitchenWares[0].wareName;
                wareImage1.sprite = currentKitchenWares[0].wareImage;
            }
            else
            {
                upgradeItemObjects[0].SetActive(false);
            }

            if (currentKitchenWares[1] != null)
            {
                upgradeItemObjects[1].SetActive(true);
                wareName2.text = currentKitchenWares[1].wareName;
                wareImage2.sprite = currentKitchenWares[1].wareImage;
            }
            else
            {
                upgradeItemObjects[1].SetActive(false);
            }

            if (currentKitchenWares[2] != null)
            {
                upgradeItemObjects[2].SetActive(true);
                wareName3.text = currentKitchenWares[2].wareName;
                wareImage3.sprite = currentKitchenWares[2].wareImage;
            }
            else
            {
                upgradeItemObjects[2].SetActive(false);
            }

            UpdateWareGradeAppearance();
        }

        void UpdateWareGradeAppearance()
        { //min. grade = 0 (no upgrades yet), max=3
            for(int i= grades1.Length-1; i >= 0; i--) //change sprite color of the grades in upgradeItem1
            {
               
                grades1[i].GetComponent<Image>().color=inactiveGradeColor;
                if (currentKitchenWares[0] != null && i+1<= currentKitchenWares[0].grade)
                {
                    grades1[i].GetComponent<Image>().color=activeGradeColor;
                }
               
            }
            for (int i = grades2.Length-1; i >= 0; i--) //change sprite color of the grades in upgradeItem2
            {
                grades2[i].GetComponent<Image>().color = inactiveGradeColor;
                if (currentKitchenWares[1] != null && i+1 <= currentKitchenWares[1].grade)
                {
                    grades2[i].GetComponent<Image>().color = activeGradeColor;
                }
            }
            for (int i = grades3.Length - 1; i >= 0; i--) //change sprite color of the grades in upgradeItem3
            {
                grades3[i].GetComponent<Image>().color = inactiveGradeColor;
                if (currentKitchenWares[2] != null && i+1 <= currentKitchenWares[2].grade)
                {
                    grades3[i].GetComponent<Image>().color = activeGradeColor;
                }
            }
        }
    }
}
