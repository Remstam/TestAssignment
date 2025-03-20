using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class EnemiesSystem : BaseSystem
    {
        public event Action<EnemySystem> EnemySpawned = delegate { };

        public List<EnemySystem> ActiveEnemies => _activeEnemies;
        
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly EnemySpawnSystem _enemySpawnSystem;
        private readonly List<EnemySystem> _activeEnemies = new();

        public EnemiesSystem(EnemySpawnerConfig enemySpawnerConfig, EnemySpawnSystem enemySpawnSystem)
        {
            _enemySpawnerConfig = enemySpawnerConfig;
            _enemySpawnSystem = enemySpawnSystem;
        }

        public void Init()
        {
            SpawnEnemies(_enemySpawnerConfig);
        }

        private void SpawnEnemies(EnemySpawnerConfig enemySpawnerConfig)
        {
            var enemiesOnBoard = enemySpawnerConfig.EnemiesOnBoard;
            for (var i = 0; i < enemiesOnBoard; i++)
            {
                SpawnNextEnemy(enemySpawnerConfig);
            }
        }

        private void SpawnNextEnemy(EnemySpawnerConfig enemySpawnerConfig)
        {
            if (_activeEnemies.Count >= enemySpawnerConfig.EnemiesOnBoard)
            {
                Debug.LogWarning($"Can't spawn enemy as there are {_activeEnemies.Count} enemies already.");
                return;
            }
            
            var enemyConfig = enemySpawnerConfig.EnemyConfigs.GetRandom();
            var enemySystem = _enemySpawnSystem.SpawnEnemy(enemyConfig);
            enemySystem.HasDied += OnDie;
            enemySystem.Init();
                
            _activeEnemies.Add(enemySystem);
            
            EnemySpawned?.Invoke(enemySystem);
        }

        private void OnDie(IBattleMember battleMember)
        {
            var enemySystem = (EnemySystem) battleMember;
            
            enemySystem.HasDied -= OnDie;
            
            _activeEnemies.Remove(enemySystem);
            _enemySpawnSystem.DespawnEnemy(enemySystem);
            
            SpawnNextEnemy(_enemySpawnerConfig);
        }

        public override void Dispose()
        {
            foreach (var enemy in _activeEnemies)
            {
                enemy.HasDied -= OnDie;
                enemy.Dispose();
            }
        }
    }
}