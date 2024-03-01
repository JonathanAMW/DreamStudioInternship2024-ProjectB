//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/29"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;



namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Scriptable object script for Generator Utensil Stats Assets
    /// </summary>
    [CreateAssetMenu(fileName = "[UtensilName]_[Level]_SO", menuName ="ScriptableObjects/UtensilStats/GeneratorUtensilStats")]
    public class GeneratorUtensilStats : ScriptableObject
    {
        [SerializeField] private GeneratorUtensilStatsData _statsData;

        public GeneratorUtensilStatsData StatsData => _statsData;
    }

    [System.Serializable]
    public struct GeneratorUtensilStatsData
    {
        
    }
}
