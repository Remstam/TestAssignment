using System;
using UnityEngine;

namespace TestGame
{
    [Serializable]
    public class PrefabInfo
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private PoolType _poolType;

        public GameObject Prefab => _prefab;
        public PoolType PoolType => _poolType;
    }
}