//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using UnderworldCafe.CookingSystem;
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
        public string[] ingredients;
        public string description;

        public Recipe recipe;
        public bool isAuto;
        public bool isCode;

        public void InitializeRecipeVisual() //assign information based on recipe
        {

            if (recipe == null)
            {
                Debug.LogError("Recipe is not assigned.");
                return;
            }
           
            
            if(isAuto) //assigns recipe visual details automatically
            {
                foodImage = recipe.RecipeInformation.RecipeOutput.IngredientInformation.IngredientSprite;
                foodName = recipe.RecipeInformation.RecipeName;
                ingredients = new string[recipe.RecipeInformation.Requirements.Count];

                if(isCode)
                    description = recipe.RecipeInformation.RecipeOutput.IngredientInformation.Description;

                for (int i = 0; i < ingredients.Length; i++)
                {
                    ingredients[i] = recipe.RecipeInformation.Requirements[i].IngredientInformation.Name;
                }
            }
           
        }

    }
}
