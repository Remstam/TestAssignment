using System.Collections.Generic;

namespace TestGame
{
    public class EnemySpawnSystem : BaseSystem
    {
        private readonly EnemyPlacementController _enemyPlacementController;
        
        private readonly Dictionary<string, PoolController> _pools;

        public EnemySpawnSystem(EnemySpawnerConfig enemySpawnerConfig,
            EnemyPlacementController enemyPlacementController, PoolSystem poolSystem)
        {
            _enemyPlacementController = enemyPlacementController;

            _pools = CreatePools(enemySpawnerConfig.EnemyConfigs, poolSystem);
        }

        private Dictionary<string, PoolController> CreatePools(List<EnemyConfig> enemyConfigs, PoolSystem poolSystem)
        {
            var result = new Dictionary<string, PoolController>();
            
            foreach (var config in enemyConfigs)
            {
                var poolAnchor = config.EnemyName;
                var pool = poolSystem.CreatePool(poolAnchor, config.PoolType, config.Prefab);
                
                result.Add(config.EnemyName, pool);
            }

            return result;
        }

        // TODO: discussable, we could pool the EnemySystem itself here
        // what if different enemy behaviour is needed?
        public EnemySystem SpawnEnemy(EnemyConfig config)
        {
            var pool = _pools[config.EnemyName];
            var enemyInstance = pool.GetOrCreate();

            _enemyPlacementController.PlaceEnemy(enemyInstance);
            
            var controller = enemyInstance.GetComponent<EnemyController>();
            var enemySystem = new EnemySystem(controller, config);
            
            return enemySystem;
        }

        public void DespawnEnemy(EnemySystem enemySystem)
        {
            var pool = _pools[enemySystem.Name];
            pool.Return(enemySystem.Object);
        }
        
        public override void Dispose()
        {
            
        }
    }
}