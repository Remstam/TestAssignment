using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class PoolsController : MonoBehaviour
    {
        [SerializeField] private Transform _root;

        private readonly Dictionary<string, PoolController> _anchors = new();
        
        public PoolController CreatePool(string anchorName, Vector3 position, GameObject prefab, int size)
        {
            if (_anchors.TryGetValue(anchorName, out var pool))
            {
                return pool;
            }

            var anchor = new GameObject();
            anchor.transform.SetParent(_root);
            anchor.transform.position = position;
            
            var newPool = anchor.AddComponent<PoolController>();
            newPool.Init(anchorName, prefab, size);
            
            _anchors.Add(anchorName, newPool);

            return newPool;
        }
        
        public bool CleanPool(string anchorName)
        {
            if (_anchors.TryGetValue(anchorName, out var pool))
            {
                pool.Clean();
                return true;
            }
            
            Debug.LogWarning($"Pool '{anchorName}' was not found to clean.");
            return false;
        }

        public bool DeletePool(string anchorName)
        {
            if (_anchors.TryGetValue(anchorName, out var pool))
            {
                pool.Clean();
                Destroy(pool);

                _anchors.Remove(anchorName);
                
                return true;
            }
            
            Debug.LogWarning($"Pool '{anchorName}' was not found to delete.");
            return false;
        }
    }
}