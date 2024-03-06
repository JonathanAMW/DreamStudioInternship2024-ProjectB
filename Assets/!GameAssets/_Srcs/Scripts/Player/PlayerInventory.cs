//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/16"
//----------------------------------------------------------------------
using System;
using System.Linq;
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
        [SerializeField] private Transform _slotSpawnPoint;
        [SerializeField] private Transform _slotPoolPoint;
        [SerializeField] private GameObject _inventorySlotPrefab;
        private ObjectPool<GameObject> _inventorySlotPool; 

        #endregion
        
        private void Awake()
        {
            // Initialize the object pool
            // Arg => Constructor, Action when getting object from pool, Action when returning object to pool
            _ingredientPool = new ObjectPool<Ingredient>(() => ScriptableObject.CreateInstance<Ingredient>(), null, null); 
            _inventorySlotPool = new ObjectPool<GameObject>(() => Instantiate(_inventorySlotPrefab), null, null);
            PlayerInventoryList = new List<Ingredient>();
        }

        public void AddInventory(Ingredient ingredientToAdd)
        {
            var newIngredient = _ingredientPool.Get();
            newIngredient.CopyIngredientInformation(ingredientToAdd);
            PlayerInventoryList.Add(newIngredient);

            //add ingredient to visual
            var newSlot = _inventorySlotPool.Get();
            newSlot.GetComponent<SpriteRenderer>().sprite = ingredientToAdd.IngredientInformation.IngredientSprite;

            //Set parent and display the slot
            newSlot.transform.SetParent(_slotSpawnPoint);
            newSlot.SetActive(true);
        }

        public void RemoveInventoryAll()
        {
            //remove ingredient from visual
            Transform[] slotTransform = _slotSpawnPoint.GetComponentsInChildren<Transform>()
                                      .Where(child => child != _slotSpawnPoint.transform)
                                      .ToArray();
            foreach(var slot in slotTransform)
            {
                slot.gameObject.SetActive(false);
                slot.transform.SetParent(_slotPoolPoint);

                _inventorySlotPool.Release(slot.gameObject);
            }

            // Return the object to the pool for each ingredient in the inventory list
            foreach (Ingredient ingredient in PlayerInventoryList)
            {
                _ingredientPool.Release(ingredient);
            }

            PlayerInventoryList.Clear();
        }

        
    }
}
