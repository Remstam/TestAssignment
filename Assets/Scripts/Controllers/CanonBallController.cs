using System;
using System.Collections;
using UnityEngine;

namespace TestGame
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class CanonBallController : MonoBehaviour
    {
        public event Action<IBattleMemberComponent, bool> OnCollided = delegate { };
        public event Action<CanonBallController> OnExploded = delegate { };

        private Rigidbody _rb;
        private Collider _collider;
        private YieldInstruction _wait;
        private bool _canCollide;
        
        private Rigidbody Rb => _rb ?? GetComponent<Rigidbody>();
        
        public void Init(CanonBallSpellConfig config)
        {
            Rb.velocity = transform.forward * config.Speed;
            _wait ??= new WaitForSeconds(config.LifeTime);

            _canCollide = true;
            StartCoroutine(WaitForExplosion());
        }

        private IEnumerator WaitForExplosion()
        {
            yield return _wait;
            DoExplosion();
        }

        private void DoExplosion()
        {
            OnExploded?.Invoke(this);
        }

        public void DeInit()
        {
            _canCollide = false;
            Rb.velocity = Vector3.zero;
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
                OnCollided?.Invoke(battleMemberComponent, true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (!_canCollide)
            {
                return;
            }
            
            var battleMemberComponent = other.gameObject.GetComponent<IBattleMemberComponent>();
            if (battleMemberComponent != null)
            {
                OnCollided?.Invoke(battleMemberComponent, false);
            }
        }
    }
}