using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public class EnemySystem : BaseSystem, IBattleMember, IChaser
    {
        public event Action<IChaser> StoppedChase = delegate { };
        public event Action<IBattleMember, IBattleMember, float> DamageSent = delegate { };
        public event Action<IBattleMember> HasDied = delegate { };

        public string Name { get; }
        public string BattleName { get; }
        public GameObject Object { get; }
        public Vector3 Position => Object.transform.position;
        public bool IsAlive() => _isAlive;

        private readonly EnemyController _enemyController;
        private readonly EnemyConfig _config;
        private readonly HashSet<IBattleMember> _activeTargets = new();
        private readonly List<IBattleMember> _toClear = new();
        private bool _canHit = true;
        private bool _isAlive = true;
        private bool _isAttackRestricted;
        private readonly YieldInstruction _hitWait;

        public EnemySystem(EnemyController enemyController, EnemyConfig config)
        {
            _enemyController = enemyController;
            _config = config;
            
            _hitWait = new WaitForSeconds(config.HitCooldown);

            Name = _config.EnemyName;
            BattleName = _enemyController.name;
            Object = _enemyController.gameObject;
        }

        public void Init()
        {
            Debug.Log($"Init {_config.EnemyName} with {_config.Health} health points");

            _enemyController.OnEnemyDie += OnEnemyDie;
            _enemyController.OnCollided += OnCollided;
            _enemyController.Init(this, _config.Health, _config.MoveSpeed);
        }

        public void SetTarget(ITarget target)
        {
            _enemyController.SetTarget(target);
        }

        public void RestrictAttack(bool isRestricted)
        {
            _isAttackRestricted = isRestricted;
        }
        
        private void OnCollided(IBattleMemberComponent battleMemberComponent, bool isActive)
        {
            var battleMember = battleMemberComponent.BattleMember;
            
            if (isActive)
            {
                _activeTargets.Add(battleMember);
            }
            else
            {
                _activeTargets.Remove(battleMember);
            }

            // TODO: copypasted from PlayerSystem, but may have different logic 
            // only player is expected for the hit, but who knows
            if (_activeTargets.Count > 0)
            {
                if (_canHit)
                {
                    _canHit = false;
                    _enemyController.StartCoroutine(Hit());
                }
            }
            else
            {
                _canHit = true;
                _enemyController.StopCoroutine(Hit());
            }
        }

        private IEnumerator Hit()
        {
            while (true)
            {
                ClearDeadTargets();
                
                if (_activeTargets.Count == 0)
                {
                    _canHit = true;
                    break;
                }
                
                var to = _activeTargets.GetRandom();
                if (!_isAttackRestricted)
                {
                    DamageSent?.Invoke(this, to, _config.Attack);
                }

                yield return _hitWait;
            }
        }

        private void ClearDeadTargets()
        {
            _toClear.Clear();
            var deadTargets = _activeTargets.Where(t => !t.IsAlive());
            foreach (var t in deadTargets)
            {
                _toClear.Add(t);
            }

            foreach (var t in _toClear)
            {
                _activeTargets.Remove(t);
            }
        }
        
        private void OnEnemyDie()
        {
            Debug.Log($"I died ({Name}).");
            
            _activeTargets.Clear();
            _isAlive = false;
            
            StoppedChase?.Invoke(this);
            HasDied?.Invoke(this);
            
            _enemyController.OnEnemyDie -= OnEnemyDie;
            _enemyController.StopCoroutine(Hit());
        }

        public override void Dispose()
        {
            if (_enemyController)
            {
                _enemyController.OnEnemyDie -= OnEnemyDie;
            }
        }

        // TODO: suspicious damage formula in test assignment, '1 - defence' is more habitual
        // TODO: can upgrade defence stats choice based on damage sender (battle member, spell, etc.)
        public void TakeDamage(string from, float damage)
        {
            if (_enemyController != null)
            {
                var realDamage = damage * (1f - _config.Defence);
                Debug.Log(
                    $"I ({Name}) take {realDamage} damage points (originally {damage} damage points) after defence from ({from}).");
                _enemyController.TakeDamage(realDamage);
            }
        }
    }
}