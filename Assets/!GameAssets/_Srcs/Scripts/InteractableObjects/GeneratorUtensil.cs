//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
    /// </summary>
    public class GeneratorUtensil : Utensil
    {
        [Header("Generator Properties")]   
        [SerializeField] private Ingredient _generatedIngredient;
        
        [SerializeField] private bool _isMultipleCarryable;

        public override void Interact()
        {
            if(!_isMultipleCarryable)
            {
                if(_playerControllerRef.PlayerInventory.PlayerInventoryList.Count != 0)
                {
                    _playerControllerRef.PlayerInventory.RemoveInventoryAll();
                    _playerControllerRef.PlayerInventory.AddInventory(_generatedIngredient);
                }
            }
            else
            {
                _playerControllerRef.PlayerInventory.AddInventory(_generatedIngredient);
            }
        }
    }
}
