using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TestGame
{
    public class Health : MonoBehaviour
    {
        public event Action OnDie = delegate { };

        public float Value => _value;
        
        private float _value;
        
        public void Init(float health)
        {
            Assert.IsTrue(health > 0, "Health is non-positive on Init call.");

            _value = health;
        }

        // TODO: health points update is not a view thing, it has to be a separate system
        // Creature death could be determined not only by the health points
        public void TakeDamage(float damage)
        {
            var newHealth = _value - damage;
            newHealth = Mathf.Clamp(newHealth, 0f, newHealth);
            _value = newHealth;
            
            if (_value <= Mathf.Epsilon)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDie?.Invoke();
        }
    }
}