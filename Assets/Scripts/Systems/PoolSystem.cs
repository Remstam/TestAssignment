using UnityEngine;

namespace TestGame
{
    public class PoolSystem : BaseSystem
    {
        private readonly PoolConfig _poolConfig;
        private readonly PoolsController _poolsController;

        public PoolSystem(PoolConfig poolConfig, PoolsController poolsController)
        {
            _poolConfig = poolConfig;
            _poolsController = poolsController;
        }

        public PoolController CreatePool(string poolAnchor, PoolType configPoolType, GameObject prefab)
        {
            var poolSettings = _poolConfig.GetPoolSettingsByType(configPoolType);
            var pool = _poolsController.CreatePool(poolAnchor, poolSettings.Position, prefab, poolSettings.Size);

            return pool;
        }
        
        public override void Dispose()
        {
            
        }
    }
}