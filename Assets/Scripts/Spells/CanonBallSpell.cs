using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public class CanonBallSpell : ISpell
    {
        public SpellType Type => SpellType.CanonBall;
        
        private readonly CanonBallSpellConfig _config;
        private readonly PlayerSystem _playerSystem;
        private readonly TimerSystem _timerSystem;
        private readonly BattleSystem _battleSystem;
        private readonly PoolController _pool;
        private readonly HashSet<CanonBall> _currentBalls = new();
        private readonly Transform _anchor;
        private bool _canCast = true;

        public CanonBallSpell(BaseSpellConfig config, PlayerSystem playerSystem, PoolSystem poolSystem, TimerSystem timerSystem, BattleSystem battleSystem)
        {
            _config = (CanonBallSpellConfig)config;
            _playerSystem = playerSystem;
            _timerSystem = timerSystem;
            _battleSystem = battleSystem;
            _pool = CreatePool(_config, poolSystem);
            _anchor = new GameObject($"{config.Type}Anchor").transform;
        }

        private PoolController CreatePool(CanonBallSpellConfig config, PoolSystem poolSystem)
        {
            var pool = poolSystem.CreatePool(config.Type.ToString(), config.PrefabInfo.PoolType, config.PrefabInfo.Prefab);
            return pool;
        }
        
        public bool Cast()
        {
            if (!_canCast)
            {
                return false;
            }
            
            var canonBall = new CanonBall(_config, _pool, _playerSystem.WeaponPosition, _playerSystem.WeaponRotation, _anchor);
            canonBall.Died += OnDied;
            
            _currentBalls.Add(canonBall);
            
            _canCast = false;
            _timerSystem.SetTimer(_config.Cooldown, () => _canCast = true);

            return true;
        }

        private void OnDied(CanonBall ball)
        {
            Debug.Log("Canon ball died.");

            var targets = _battleSystem.BattleMembers.Where(x => IsInDamageRadius(x, ball, _config.DamageRadius)).ToList();
            foreach (var target in targets)
            {
                target.TakeDamage(ball.Name, _config.Damage);
            }
            
            ball.Died -= OnDied;
            ball.DeInit();
            
            _currentBalls.Remove(ball);
        }

        private bool IsInDamageRadius(IBattleMember battleMember, CanonBall ball, float damageRadius)
        {
            return (battleMember.Position - ball.Position).sqrMagnitude <= damageRadius * damageRadius;
        }
        
        public void Dispose()
        {
            foreach (var ball in _currentBalls)
            {
                ball.Died -= OnDied;
                ball.DeInit();
            }
        }
    }
}