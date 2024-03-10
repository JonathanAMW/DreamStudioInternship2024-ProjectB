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
            public string Description;
            public Sprite IngredientSprite;


            // // Custom Equals method for the IngredientInformations struct
            // public bool Equals(IngredientInformations other)
            // {
            //     return Name == other.Name &&
            //         Description == other.Description &&
            //         IngredientSprite == other.IngredientSprite;
            // }

            // // Custom GetHashCode method for the IngredientInformations struct
            // public override int GetHashCode()
            // {
            //     unchecked // Overflow is fine, just wrap
            //     {
            //         int hash = 17;
            //         hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
            //         hash = hash * 23 + (Description != null ? Description.GetHashCode() : 0);
            //         hash = hash * 23 + (IngredientSprite != null ? IngredientSprite.GetHashCode() : 0);
            //         return hash;
            //     }
            // }
        }

        [SerializeField] private IngredientInformations _ingredientInformation;

        public IngredientInformations IngredientInformation => _ingredientInformation;

        // public void CopyIngredientInformation(Ingredient ingredientToCopy)
        // {
        //     _ingredientInformation = ingredientToCopy.IngredientInformation;
        // }

        // public override bool Equals(object other)
        // {
        //     if (!(other is Ingredient))
        //         return false;

        //     Ingredient otherIngredient = (Ingredient)other;
        //     return _ingredientInformation.Equals(otherIngredient.IngredientInformation);
        // }

        // public override int GetHashCode()
        // {
        //     return _ingredientInformation.GetHashCode();
        // }

        private void OnValidate()
        {
            if(string.IsNullOrEmpty(_ingredientInformation.Name))
            {
                Debug.LogError("No name has been set on ingredient: " + name);
            }
            if(string.IsNullOrEmpty(_ingredientInformation.Description))
            {
                Debug.LogError("No name has been set on ingredient: " + name);
            }
            if(_ingredientInformation.IngredientSprite == null)
            {
                Debug.LogError("No Sprite has been set on ingredient: " + name);
            }
        }
    }
}