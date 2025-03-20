using UnityEngine;

namespace TestGame
{
    public class PoolController : MonoBehaviour
    {
        private int _size;
        private GameObject _prefab;
        
        public void Init(string anchorName, GameObject prefab, int size)
        {
            name = anchorName;
            _prefab = prefab;
            _size = size;
        }

        public GameObject GetOrCreate()
        {
            if (transform.childCount == 0)
            {
                var newGo = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
                return newGo;
            }

            var go = transform.GetChild(0).gameObject;
            go.SetActive(true);
            
            return go;
        }

        public bool Return(GameObject go)
        {
            if (transform.childCount >= _size)
            {
                Debug.LogWarning($"There is no place in the pool. Object will be destroyed.");
                Destroy(go);
                return false;
            }
            
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.SetActive(false);

            return true;
        }

        public void Clean()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}