using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class MindShatterSpell : ISpell
    {
        public SpellType Type => SpellType.MindShatter;
        
        private readonly MindShatterConfig _config;
        private readonly TimerSystem _timerSystem;
        private readonly TargetSystem _targetSystem;
        private readonly EnemiesSystem _enemiesSystem;
        private readonly FenceSystem _fenceSystem;
        private readonly List<ITarget> _targets;
        private bool _canCast = true;
        private List<EnemySystem> _enemies;

        public MindShatterSpell(BaseSpellConfig config, TimerSystem timerSystem, TargetSystem targetSystem,
            EnemiesSystem enemiesSystem, FenceSystem fenceSystem)
        {
            _config = (MindShatterConfig)config;
            _timerSystem = timerSystem;
            _targetSystem = targetSystem;
            _enemiesSystem = enemiesSystem;
            _fenceSystem = fenceSystem;

            _targets = new List<ITarget>(_config.PointsCount);
        }
        
        public bool Cast()
        {
            if (!_canCast)
            {
                return false;
            }
            
            GenerateRandomTargets();
            RegisterTargets(_targets);

            _enemies = new List<EnemySystem>(_enemiesSystem.ActiveEnemies);
            DisableAttack(_enemies);
            
            _canCast = false;
            _timerSystem.SetTimer(_config.Cooldown, () =>
            {
                UnregisterTargets(_targets);
                EnableAttack(_enemies);
                
                _enemies.Clear();
                _targets.Clear();
                _canCast = true;
            });

            return true;
        }
        
        private void GenerateRandomTargets()
        {
            var borders = _fenceSystem.MoveBorders;
            
            for (var i = 0; i < _config.PointsCount; i++)
            {
                var randX = Random.Range(borders.left, borders.right);
                var randZ = Random.Range(borders.down, borders.up);
                var position = new Vector3(randX, 0f, randZ);

                _targets.Add(new MindShatterPoint() {Position = position});
            }
        }

        private void RegisterTargets(List<ITarget> targets)
        {
            foreach (var t in targets)
            {
                _targetSystem.RegisterTarget(t);
            }
        }

        private void UnregisterTargets(List<ITarget> targets)
        {
            foreach (var t in targets)
            {
                _targetSystem.UnregisterTarget(t);
            }
        }

        private void DisableAttack(List<EnemySystem> enemies)
        {
            foreach (var e in enemies)
            {
                e.RestrictAttack(true);
                e.HasDied += OnDied;
            }
        }

        private void EnableAttack(List<EnemySystem> enemies)
        {
            foreach (var e in enemies)
            {
                e.RestrictAttack(false);
                e.HasDied -= OnDied;
            }
        }
        
        private void OnDied(IBattleMember battleMember)
        {
            var enemy = (EnemySystem) battleMember;
            enemy.HasDied -= OnDied;

            enemy.RestrictAttack(false);
            _enemies.Remove(enemy);
        }

        public void Dispose()
        {
            UnregisterTargets(_targets);
            EnableAttack(_enemies);
                
            _enemies.Clear();
            _targets.Clear();
        }
    }
}