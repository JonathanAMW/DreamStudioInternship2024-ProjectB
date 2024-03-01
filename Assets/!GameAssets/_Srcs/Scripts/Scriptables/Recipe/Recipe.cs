//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Class is for templates to create recipes
    /// </summary>
    [CreateAssetMenu(fileName = "[RecipeName]_SO", menuName ="ScriptableObjects/Recipe")]
    public class Recipe : ScriptableObject
    {
        [System.Serializable]
        public struct RecipeInformations
        {
            public string RecipeName;
            public Ingredient RecipeOutput;
            public List<Ingredient> Requirements;
        }

        [SerializeField] private RecipeInformations _recipeInformation;
        public RecipeInformations RecipeInformation => _recipeInformation;

    }
}
