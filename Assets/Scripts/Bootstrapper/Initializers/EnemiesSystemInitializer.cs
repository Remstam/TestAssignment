using System.Threading.Tasks;

namespace TestGame
{
    public class EnemiesSystemInitializer : BaseSystemInitializer<EnemiesSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var enemySpawnSystem = ServiceContainer.Get<EnemySpawnSystem>();

            var enemySystem = new EnemiesSystem(configSystem.EnemySpawnerConfig, enemySpawnSystem);

            return Task.FromResult((BaseSystem) enemySystem);
        }
    }
}