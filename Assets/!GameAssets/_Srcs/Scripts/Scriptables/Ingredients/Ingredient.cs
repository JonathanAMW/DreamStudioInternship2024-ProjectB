//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/10/05"
//----------------------------------------------------------------------

using System.ComponentModel;
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

            [TextArea]
            public string Description;
            public Sprite IngredientSprite;
        }

        [SerializeField] private IngredientInformations _ingredientInformation;

        public IngredientInformations IngredientInformation => _ingredientInformation;


        private void OnValidate()
        {
            if(string.IsNullOrEmpty(_ingredientInformation.Name))
            {
                Debug.LogWarning("No name has been set on ingredient: " + name);
            }
            if(string.IsNullOrEmpty(_ingredientInformation.Description))
            {
                Debug.LogWarning("No name has been set on ingredient: " + name);
            }
            if(_ingredientInformation.IngredientSprite == null)
            {
                Debug.LogWarning("No Sprite has been set on ingredient: " + name);
            }
        }
    }
}