using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour, IBattleMemberComponent
    {
        public event Action OnEnemyDie = delegate { };
        public event Action<IBattleMemberComponent, bool> OnCollided = delegate { };
        private bool _canCollide;
        
        [SerializeField] private float _updateTargetRefreshTime = 0.5f;

        public IBattleMember BattleMember { get; private set; }
        public Health Health => _health ?? GetComponent<Health>();

        private YieldInstruction _waitTargetRefresh;
        private string _name;
        private Health _health;
        private NavMeshAgent _agent;
        private ITarget _target;
        private NavMeshAgent Agent => _agent ?? GetComponent<NavMeshAgent>();

        private void Start()
        {
            _waitTargetRefresh = new WaitForSeconds(_updateTargetRefreshTime);
        }

        public void Init(IBattleMember battleMember, float health, float speed)
        {
            BattleMember = battleMember;
            
            Agent.speed = speed;
            StartCoroutine(UpdateTarget());

            _canCollide = true;
            
            Health.Init(health);
            Health.OnDie += OnDie;
        }

        private IEnumerator UpdateTarget()
        {
            while (true)
            {
                if (_target != null)
                {
                    Agent.SetDestination(_target.Position);
                }

                yield return _waitTargetRefresh;
            }
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
            Agent.isStopped = _target == null;
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }

        private void OnDie()
        {
            _canCollide = false;
            
            OnEnemyDie?.Invoke();
            Health.OnDie -= OnDie;
            
            StopCoroutine(UpdateTarget());
        }

        private void OnDestroy()
        {
            if (Health != null)
            {
                Health.OnDie -= OnDie;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_canCollide)
            {
                return;
            }
            
            
            var battleMember = other.gameObject.GetComponent<IBattleMemberComponent>();
            if (battleMember != null)
            {
                OnCollided?.Invoke(battleMember, true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (!_canCollide)
            {
                return;
            }
            
            var battleMember = other.gameObject.GetComponent<IBattleMemberComponent>();
            if (battleMember != null)
            {
                OnCollided?.Invoke(battleMember, false);
            }
        }
    }
}