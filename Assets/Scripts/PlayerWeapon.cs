using System;
using UnityEngine;

namespace TestGame
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerWeapon : MonoBehaviour
    {
        public event Action<Collision, bool> OnCollision = delegate { };
        
        [SerializeField] private Transform _spawnPoint;

        public Transform SpawnPoint => _spawnPoint;
        private void OnCollisionEnter(Collision other)
        {
            OnCollision?.Invoke(other, true);
        }

        private void OnCollisionExit(Collision other)
        {
            OnCollision?.Invoke(other, false);
        }
    }
}