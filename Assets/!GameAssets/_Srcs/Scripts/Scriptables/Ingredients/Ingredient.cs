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
    [CreateAssetMenu(fileName = "[IngredientName]_SO", menuName ="ScriptableObjects/Ingredient")]
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

        public void CopyIngredientInformation(Ingredient ingredientToCopy)
        {
            _ingredientInformation = ingredientToCopy.IngredientInformation;
        }
    }
}