using System.Threading.Tasks;

namespace TestGame
{
    public class DebugSystemInitializer : BaseSystemInitializer<DebugSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var inputSystem = ServiceContainer.Get<InputSystem>();
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var battleSystem = ServiceContainer.Get<BattleSystem>();

            var debugSystem = new DebugSystem(inputSystem, configSystem.DebugConfig, battleSystem);

            return Task.FromResult((BaseSystem) debugSystem);
        }
    }
}