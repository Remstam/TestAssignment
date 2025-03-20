using System.Threading.Tasks;

namespace TestGame
{
    public class DebugWindowInitializer : BaseSystemInitializer<DebugWindow>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var debugWindowController = ServiceContainer.Get<DebugWindowController>();
            var playerSystem = ServiceContainer.Get<PlayerSystem>();
            var spellsSystem = ServiceContainer.Get<SpellsSystem>();
            var configSystem = ServiceContainer.Get<ConfigSystem>();

            var debugWindow = new DebugWindow(debugWindowController, playerSystem, spellsSystem, configSystem);
            debugWindow.Init();

            return Task.FromResult((BaseSystem) debugWindow);
        }
    }
}