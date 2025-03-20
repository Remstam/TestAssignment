using System;
using UnityEngine;

namespace TestGame
{
    [Serializable]
    public class PoolSettings
    {
        [SerializeField] private PoolType _poolType;
        [SerializeField] private int _size;
        [SerializeField] private Vector3 _position;

        public PoolType Type => _poolType;
        public int Size => _size;
        public Vector3 Position => _position;
    }
}