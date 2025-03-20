using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "TestGame/Enemy/Spawner Config")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        private const int MaxEnemies = 20;
        
        [SerializeField] private List<EnemyConfig> _enemyConfigs = new();
        [SerializeField, Min(1)] private int _enemiesOnBoard = 10;

        public List<EnemyConfig> EnemyConfigs => _enemyConfigs;
        public int EnemiesOnBoard => _enemiesOnBoard;

        private void OnValidate()
        {
            _enemiesOnBoard = Mathf.Clamp(_enemiesOnBoard, 1, MaxEnemies);
            _enemyConfigs.RemoveAll(x => !x);
        }
    }
}