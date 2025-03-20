using System.Threading.Tasks;

namespace TestGame
{
    public class EnemySpawnSystemInitializer : BaseSystemInitializer<EnemySpawnSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var enemyPlacementController = ServiceContainer.Get<EnemyPlacementController>();
            var poolSystem = ServiceContainer.Get<PoolSystem>();

            var enemySpawnSystem =
                new EnemySpawnSystem(configSystem.EnemySpawnerConfig, enemyPlacementController, poolSystem);

            return Task.FromResult((BaseSystem) enemySpawnSystem);
        }
    }
}