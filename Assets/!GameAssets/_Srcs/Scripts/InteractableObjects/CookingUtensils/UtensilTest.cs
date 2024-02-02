//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/10/22"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;


namespace UnderworldCafe.UtensilSystem
{
    /// <summary>
    /// Test class for appliances / kitchen utils scripts
    /// </summary>
    public class UtensilTest : Utensil
    {
        public override void Interact()
        {
            Debug.Log("Using Utensil");
        }
    }
}
