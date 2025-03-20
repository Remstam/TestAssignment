using System.Collections.Generic;
using System.Linq;

namespace TestGame
{
    public class TargetSystem : BaseSystem
    {
        private readonly ITarget _playerSystem;
        private readonly EnemiesSystem _enemiesSystem;
        private readonly List<ITarget> _targets = new();
        private readonly List<IChaser> _chasers = new();

        public TargetSystem(ITarget playerSystem, EnemiesSystem enemiesSystem)
        {
            _playerSystem = playerSystem;
            _enemiesSystem = enemiesSystem;
        }

        public void Init()
        {
            RegisterTarget(_playerSystem);

            _enemiesSystem.EnemySpawned += OnEnemySpawned;
            foreach (var chaser in _enemiesSystem.ActiveEnemies)
            {
                OnEnemySpawned(chaser);
            }
        }

        private void OnEnemySpawned(IChaser chaser)
        {
            _chasers.Add(chaser);
            chaser.StoppedChase += OnStoppedChase;
            
            SetTarget(chaser);
        }

        private void OnStoppedChase(IChaser chaser)
        {
            _chasers.Remove(chaser);

            chaser.SetTarget(null);
            chaser.StoppedChase -= OnStoppedChase;
        }
        
        private void SetTarget(IChaser chaser)
        {
            var target = GetTarget(chaser);
            chaser.SetTarget(target);
        }
        
        public void RegisterTarget(ITarget target)
        {
            if (_targets.Contains(target))
            {
                return;
            }
            
            _targets.Add(target);

            foreach (var chaser in _chasers)
            {
                SetTarget(chaser);
            }
        }

        public void UnregisterTarget(ITarget target)
        {
            _targets.Remove(target);
            
            foreach (var chaser in _chasers)
            {
                SetTarget(chaser);
            }
        }

        private ITarget GetTarget(IChaser chaser)
        {
            if (_targets.Count == 0)
            {
                return null;
            }

            if (_targets.Count == 1)
            {
                return _targets[0];
            }

            var highestPriority = _targets.Max(x => x.Priority);
            var availableTargets = _targets.Where(x => x.Priority == highestPriority).ToList();
            
            return availableTargets.GetRandom();
            
            //TODO: maybe we want distance-based target pick up
        }
        
        public override void Dispose()
        {
            _enemiesSystem.EnemySpawned -= OnEnemySpawned;
            foreach (var chaser in _chasers)
            {
                if (chaser == null)
                {
                    continue;
                }

                chaser.StoppedChase -= OnStoppedChase;
            }
            
            _chasers.Clear();
        }
    }
}