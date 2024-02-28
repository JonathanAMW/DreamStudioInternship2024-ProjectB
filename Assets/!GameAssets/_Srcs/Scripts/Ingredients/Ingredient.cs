//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/10/05"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Class is for ingredient script to create Scriptable object assets
    /// </summary>
    public class Ingredient : ScriptableObject
    {
        [System.Serializable]
        public struct IngredientInformations
        {
            public string Name;
            public string Description;
            public Sprite IngredientSprite;
        }

        [SerializeField] private IngredientInformations _ingredientInformation;

        public IngredientInformations IngredientInformation => _ingredientInformation;
    }
}

//FOR RECIPE SYSTEM
//you can try make each utensil has its own recipe
// if the recipe is needed to be compiled as one collention (e.g one universal recipe, try get all utensil)