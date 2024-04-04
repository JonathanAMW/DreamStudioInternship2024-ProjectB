//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using UnderworldCafe.CookingSystem;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Scriptableobject for recipe page
    /// </summary>
    [CreateAssetMenu(fileName = "Page", menuName = "ScriptableObjects/RecipePage", order = 2)]
    public class RecipePage : ScriptableObject
    {

       
        public FoodRecipe[] recipes = new FoodRecipe[2];
    }
}
