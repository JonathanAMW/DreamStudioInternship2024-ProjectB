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

        [SerializeField] private List<Ingredient> _playerInventoryList;
        public List<Ingredient> PlayerInventoryList => _playerInventoryList;


        #region Visual
        [SerializeField] private GameObject _slotSpawnPointObject;
        [SerializeField] private List<GameObject> _slotObjectInScene;
        [SerializeField] private GameObject _inventorySlotPrefab;
        #endregion
        
        //Validating Only
        private void OnValidate()
        {
            if(_slotSpawnPointObject == null)
            {
                Debug.LogWarning("No slot spawn point has been set on " + gameObject.name);
            }
            if(_slotObjectInScene == null)
            {
                Debug.LogWarning("No slot has been set on " + gameObject.name);
            }
            if(_inventorySlotPrefab == null)
            {
                Debug.LogWarning("No slot prefab has been set on " + gameObject.name);
            }
        }


        private void Awake()
        {
            _playerInventoryList = new List<Ingredient>();

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
            // newIngredient.IngredientInformation = ingredientToAdd.IngredientInformation;
            // newIngredient = ingredientToAdd;
            newIngredient.Init(ingredientToAdd.IngredientInformation);
            Debug.Log("Ingredient added to inventory: " + newIngredient.IngredientInformation.Name);

            _playerInventoryList.Add(newIngredient);

            //visual or front-end
            for(int i = 0; i < _slotObjectInScene.Count; i++)
            {
                if(!_slotObjectInScene[i].activeSelf)
                {
                    _slotObjectInScene[i].GetComponent<SpriteRenderer>().sprite = newIngredient.IngredientInformation.IngredientSprite;
                    _slotObjectInScene[i].SetActive(true);
                    return;
                }
            }

            //Create new slot if all slot is full
            var newSlot = Instantiate(_inventorySlotPrefab, _slotSpawnPointObject.transform);
            _slotObjectInScene.Add(newSlot);
            newSlot.GetComponent<SpriteRenderer>().sprite = newIngredient.IngredientInformation.IngredientSprite;
            newSlot.SetActive(true);
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
            foreach (Ingredient ingredient in _playerInventoryList)
            {
                Debug.Log("Ingredient removed from inventory: " + ingredient.IngredientInformation.Name);
                _ingredientPool.Release(ingredient);
            }

            _playerInventoryList.Clear();
        }

        
    }
}
