using System.Threading.Tasks;

namespace TestGame
{
    public class TargetSystemInitializer : BaseSystemInitializer<TargetSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var playerSystem = ServiceContainer.Get<PlayerSystem>();
            var enemiesSystem = ServiceContainer.Get<EnemiesSystem>();
            var targetSystem = new TargetSystem(playerSystem, enemiesSystem);
            targetSystem.Init();

            return Task.FromResult((BaseSystem) targetSystem);
        }
    }
}