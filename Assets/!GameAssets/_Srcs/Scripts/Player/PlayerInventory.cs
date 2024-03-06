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
        [SerializeField] private GameObject _slotSpawnPointObject;
        [SerializeField] private List<GameObject> _slotObjectInScene;
        [SerializeField] private GameObject _inventorySlotPrefab;
        #endregion
        
        private void Awake()
        {
            PlayerInventoryList = new List<Ingredient>();

            // Initialize the object pool
            // Arg => Constructor, Action when getting object from pool, Action when returning object to pool
            _ingredientPool = new ObjectPool<Ingredient>(() => ScriptableObject.CreateInstance<Ingredient>(), null, null); 

            // Initialize the slots
            _slotObjectInScene = new List<GameObject>();
            var newSlot = Instantiate(_inventorySlotPrefab, _slotSpawnPointObject.transform); // Create the first placeholder slot
            newSlot.SetActive(false);
            _slotObjectInScene.Add(newSlot);
        }

        public void AddInventory(Ingredient ingredientToAdd)
        {
            //Back-end
            var newIngredient = _ingredientPool.Get();
            newIngredient.CopyIngredientInformation(ingredientToAdd);
            PlayerInventoryList.Add(newIngredient);


            //visual or front-end
            for(int i = 0; i < _slotObjectInScene.Count; i++)
            {
                Debug.Log("Checking slot " + i);
                if(!_slotObjectInScene[i].activeSelf)
                {
                    _slotObjectInScene[i].GetComponent<SpriteRenderer>().sprite = ingredientToAdd.IngredientInformation.IngredientSprite;
                    _slotObjectInScene[i].SetActive(true);
                    break;
                }

                //Create new slot if all slot is full
                if(i >= _slotObjectInScene.Count - 1)
                {
                    Debug.Log("Creating new slot");
                    var newSlot = Instantiate(_inventorySlotPrefab, _slotSpawnPointObject.transform);
                    _slotObjectInScene.Add(newSlot);
                    newSlot.GetComponent<SpriteRenderer>().sprite = ingredientToAdd.IngredientInformation.IngredientSprite;
                    newSlot.SetActive(true);
                    Debug.Log("Creating new slot finished");
                    break;
                }
            }
        }

        public void RemoveInventoryAll()
        {
            //turn off all slot visual
            foreach(var slot in _slotObjectInScene)
            {
                if(slot.activeSelf)
                {
                    slot.SetActive(false);
                }
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
