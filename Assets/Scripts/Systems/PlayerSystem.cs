using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public class PlayerSystem : BaseSystem, ITarget, IBattleMember
    {
        public event Action<IBattleMember, IBattleMember, float> DamageSent = delegate { };
        public event Action<IBattleMember> HasDied = delegate { };
        public event Action TookDamage = delegate { };

        public int Priority => 0;
        public string BattleName => "Player";
        public Vector3 Position => _playerController.Position;
        public Quaternion Rotation => _playerController.Rotation;
        public Vector3 WeaponPosition => _playerController.WeaponPosition;
        public Quaternion WeaponRotation => _playerController.WeaponRotation;
        public bool IsAlive() => _isAlive;
        public float Health => _playerController.Health.Value;

        private readonly InputSystem _inputSystem;
        private readonly PlayerConfig _playerConfig;
        private readonly FenceSystem _fenceSystem;
        private readonly PlayerController _playerController;
        private Vector3 _move;
        private bool _canHit = true;
        private bool _isAlive = true;
        private readonly List<IBattleMember> _toClear = new();
        private readonly HashSet<IBattleMember> _activeTargets = new();
        private readonly YieldInstruction _hitWait;

        public PlayerSystem(InputSystem inputSystem, PlayerConfig playerConfig, FenceSystem fenceSystem,
            PlayerController playerController)
        {
            _inputSystem = inputSystem;
            _playerConfig = playerConfig;
            _fenceSystem = fenceSystem;
            _playerController = playerController;
            _hitWait = new WaitForSeconds(playerConfig.HitCooldown);
            
            _inputSystem.PlayerKeyboardSubscriber.OnKeyActions += OnKeyActions;
        }

        public void Init()
        {
            _playerController.Init(this, _playerConfig, _fenceSystem.MoveBorders);
            _playerController.OnPlayerDie += OnDie;
            _playerController.OnCollided += OnCollided;
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

            // TODO: melee attack was not clearly specified in assignment description,
            // so choose one from anybody who entered collision before the hit
            if (_activeTargets.Count > 0)
            {
                if (_canHit)
                {
                    _canHit = false;
                    _playerController.StartCoroutine(Hit());
                }
            }
            else
            {
                _canHit = true;
                _playerController.StopCoroutine(Hit());
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
                DamageSent?.Invoke(this, to, _playerConfig.Attack);
                
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
        
        private void OnDie()
        {
            Debug.Log($"I died (Player).");
            
            _activeTargets.Clear();
            _isAlive = false;
            
            HasDied?.Invoke(this);
            
            _playerController.OnPlayerDie -= OnDie;
            _playerController.OnCollided -= OnCollided;
            _playerController.StopCoroutine(Hit());
        }

        private void OnKeyActions(List<KeyAction> actions)
        {
            _move = Vector3.zero;
            foreach (var action in actions)
            {
                if (action == KeyAction.MoveUp)
                {
                    _move += Vector3.forward;
                }

                if (action == KeyAction.MoveLeft)
                {
                    _move += Vector3.left;
                }

                if (action == KeyAction.MoveDown)
                {
                    _move += Vector3.back;
                }

                if (action == KeyAction.MoveRight)
                {
                    _move += Vector3.right;
                }
            }

            if (_move != Vector3.zero)
            {
                _playerController?.Move(_move);
            }
        }

        // TODO: suspicious damage formula in test assignment, '1 - defence' is more habitual
        public void TakeDamage(string from, float damage)
        {
            if (_playerController != null)
            {
                var realDamage = damage * (1f - _playerConfig.Defence);
                Debug.Log(
                    $"I ({_playerController.name}) take {realDamage} damage points (originally {damage} damage points) after defence from ({from}).");
                _playerController.TakeDamage(realDamage);
                TookDamage?.Invoke();
            }
        }

        public override void Dispose()
        {
            _inputSystem.PlayerKeyboardSubscriber.OnKeyActions -= OnKeyActions;
            _playerController.OnPlayerDie -= OnDie;
            _playerController.OnCollided -= OnCollided;
        }
    }
}