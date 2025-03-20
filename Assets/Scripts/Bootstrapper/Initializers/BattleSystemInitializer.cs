using System.Threading.Tasks;

namespace TestGame
{
    public class BattleSystemInitializer : BaseSystemInitializer<BattleSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var playerSystem = ServiceContainer.Get<PlayerSystem>();
            var enemiesSystem = ServiceContainer.Get<EnemiesSystem>();

            var battleSystem = new BattleSystem(playerSystem, enemiesSystem);
            battleSystem.Init();

            return Task.FromResult((BaseSystem) battleSystem);
        }
    }
}