//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/29"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;



namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Scriptable object script for Converter Utensil Stats Assets
    /// </summary>
    [CreateAssetMenu(fileName = "[UtensilName]_[Level]_SO", menuName ="ScriptableObjects/UtensilStats/ConverterUtensilStats")]
    public class ConverterUtensilStats : ScriptableObject
    {
        [SerializeField] private ConverterUtensilStatsData _statsData;

        public ConverterUtensilStatsData StatsData => _statsData;
    }

    [System.Serializable]
    public struct ConverterUtensilStatsData
    {
        public List<Recipe> RecipeList;
        public int ConvertingTime;
    }
}
