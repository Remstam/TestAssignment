using System.Threading.Tasks;

namespace TestGame
{
    public class PlayerSystemInitializer : BaseSystemInitializer<PlayerSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var inputSystem = ServiceContainer.Get<InputSystem>();
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var playerController = ServiceContainer.Get<PlayerController>();
            var fenceSystem = ServiceContainer.Get<FenceSystem>();

            var playerSystem = new PlayerSystem(inputSystem, configSystem.PlayerConfig, fenceSystem, playerController);
            playerSystem.Init();
            
            return Task.FromResult((BaseSystem)playerSystem);
        }
    }
}