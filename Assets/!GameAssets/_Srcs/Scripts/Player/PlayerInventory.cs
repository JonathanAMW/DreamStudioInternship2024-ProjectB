//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/16"
//----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using UnderworldCafe.CookingSystem;


namespace UnderworldCafe.Player
{
    /// <summary>
    /// Class that responsible to manage players inventory
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        private ObjectPool<Ingredient> _ingredientPool;
        public List<Ingredient> PlayerInventoryList { get; private set; }

        #region Visual
        

        #endregion
        
        private void Awake()
        {
            // Initialize the object pool
            // Arg => Constructor, Action when getting object from pool, Action when returning object to pool, Default Capacity
            _ingredientPool = new ObjectPool<Ingredient>(() => new Ingredient(), null, null, defaultCapacity: 10); 
            PlayerInventoryList = new List<Ingredient>();
        }

        public void AddInventory(Ingredient ingredientToAdd)
        {
            var temp = _ingredientPool.Get();
            temp.CopyIngredientInformation(ingredientToAdd);
        }

        public void RemoveInventoryAll()
        {
            // Return the object to the pool for each ingredient in the inventory list
            foreach (Ingredient ingredient in PlayerInventoryList)
            {
                _ingredientPool.Release(ingredient);
            }

            PlayerInventoryList.Clear();
        }
    }
}
