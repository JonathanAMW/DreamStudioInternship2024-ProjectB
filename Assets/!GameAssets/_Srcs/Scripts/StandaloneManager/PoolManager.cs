//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/03/21"
//----------------------------------------------------------------------

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;


namespace UnderworldCafe
{
    /// <summary>
    /// Class is for managing pool in game
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        public Dictionary<string, IObjectPool<GameObject>> GameObjectPools;


        #region MonoBehavior
        private void Start()
        {
            GameObjectPools = new Dictionary<string, IObjectPool<GameObject>>();
        }
        #endregion


        public void TryAddToPool(string id, GameObject prefab)
        {
            if(!GameObjectPools.ContainsKey(id))
            {
                IObjectPool<GameObject> temp = new ObjectPool<GameObject>(() => Instantiate<GameObject>(prefab), null, null); 
                
                GameObjectPools.Add(id, temp);
            }
        }


        public GameObject TryGetFromPool(string id)
        {
            if(!GameObjectPools.ContainsKey(id))
            {
                return null;
            }
            return GameObjectPools[id].Get();
        }


        public void TryReleaseToPool(string id, GameObject obj)
        {
            if (!GameObjectPools.ContainsKey(id))
            {
                Debug.LogWarning($"Object pool with ID '{id}' does not exist.");
                return;
            }
            GameObjectPools[id].Release(obj);
        }
    }
}
