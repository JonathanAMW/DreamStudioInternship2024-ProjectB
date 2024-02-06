//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// Scriptable object script for Food Recipes
    /// </summary>
    [CreateAssetMenu(fileName = "Dish", menuName ="ScriptableObjects/FoodRecipe", order =1)]
    public class FoodRecipe : ScriptableObject
    {
        public Sprite foodImage;
        public string foodName;
        public string[] ingredients = new string[6];
        
    }
}
