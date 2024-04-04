//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/19"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnderworldCafe.DataPersistenceSystem;


namespace UnderworldCafe.CookingSystem
{
    /// <summary>
    /// Base class for every appliances / kitchen utils scripts
    /// </summary>
    public abstract class Utensil : QueuedInteractableObject
    {
        #region Utensil Information
        [Header("=======[Utensil Information]=======")] 

        [SerializeField] private UtensilInformation _utensilInformation;
        public UtensilInformation UtensilInformations => _utensilInformation;

        
        [System.Serializable]
        public struct UtensilInformation
        {
            public string Name;

            [TextArea]
            public string Description;
        }
        #endregion


        #region Utensil Properties 
        [Header("=======[Utensil General Properties]=======")]        
        [SerializeField] protected Ingredient FailedFood;
        [SerializeField] private Animator _utensilAnimator;
        #endregion
            
        protected override void OnValidate()
        {
            base.OnValidate();
            
            if(string.IsNullOrEmpty(_utensilInformation.Name))
            {
                Debug.LogWarning("No name has been set on utensil: " + gameObject.name);
            }
            if(string.IsNullOrEmpty(_utensilInformation.Description))
            {
                Debug.LogWarning("No description has been set on utensil: " + gameObject.name);
            }
            if(FailedFood == null)
            {
                Debug.LogWarning("No failed food has been set on utensil: " + gameObject.name);
            }
            // if(_utensilAnimator == null)
            // {
            //     Debug.LogWarning("No animator has been set on utensil: " + gameObject.name);
            // }
            
        }

        protected override void Awake()
        {
            base.Awake();
            
            _audioManagerRef = GameManager.Instance.AudioManager;
        }


        protected static class UtensilComparer
        {
            public static bool IsEqualWithSameOrder<T>(IEnumerable<T> list1, IEnumerable<T> list2, Func<T, object> propertySelector)
            {
                if(list1.Count() != list2.Count()) return false;

                
                // Get enumerators for both lists
                var enumerator1 = list1.GetEnumerator();
                var enumerator2 = list2.GetEnumerator();

                // Iterate over the elements of both lists
                while (enumerator1.MoveNext() && enumerator2.MoveNext())
                {
                    // Get the selected property for the current elements
                    var property1 = propertySelector(enumerator1.Current);
                    var property2 = propertySelector(enumerator2.Current);

                    // Compare the selected properties
                    if (!property1.Equals(property2))
                    {
                        // If properties are not equal, return false
                        return false;
                    }
                }

                // If all properties are equal, return true
                return true;
            }
        }
    }
}
