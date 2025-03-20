using System;
using UnityEngine;

namespace TestGame
{
    [RequireComponent(typeof(Health))]
    public class DoppelgangerController : MonoBehaviour, IBattleMemberComponent
    {
        public event Action OnDied = delegate { };
        public IBattleMember BattleMember { get; private set; }
        
        public Health Health => _health ?? GetComponent<Health>();
        private Health _health;
        
        public void Init(IBattleMember battleMember, DoppelgangerConfig config)
        {
            BattleMember = battleMember;
               
            Health.Init(config.Health);
            Health.OnDie += OnDie;
        }
        
        private void OnDie()
        {
            OnDied?.Invoke();
            Health.OnDie -= OnDie;
        }
        
        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }
        
        private void OnDestroy()
        {
            if (Health != null)
            {
                Health.OnDie -= OnDie;
            }
        }
    }
}