using System;
using System.Collections;
using UnityEngine;

namespace TestGame
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class BulletController : MonoBehaviour
    {
        public event Action<IBattleMemberComponent> OnCollided = delegate { };
        public event Action<BulletController> OnDied = delegate { };

        private Rigidbody _rb;
        private Collider _collider;
        private YieldInstruction _wait;
        private bool _canCollide;
        
        private Rigidbody Rb => _rb ?? GetComponent<Rigidbody>();
        
        public void Init(BulletSpellConfig config)
        {
            Rb.velocity = transform.forward * config.Speed;
            _wait ??= new WaitForSeconds(config.LifeTime);
            
            _canCollide = true;
            StartCoroutine(WaitForDeath());
        }
        
        private IEnumerator WaitForDeath()
        {
            yield return _wait;
            Die();
        }
        
        private void Die()
        {
            OnDied?.Invoke(this);
        }
        
        public void DeInit()
        {
            _canCollide = false;
            Rb.velocity = Vector3.zero;
            StopCoroutine(WaitForDeath());
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!_canCollide)
            {
                return;
            }
            
            var battleMemberComponent = other.gameObject.GetComponent<IBattleMemberComponent>();
            if (battleMemberComponent != null)
            {
                OnCollided?.Invoke(battleMemberComponent);
                Die();
            }
        }
    }
}