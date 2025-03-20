using System.Threading.Tasks;

namespace TestGame
{
    public class CameraSystemInitializer : BaseSystemInitializer<CameraSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var inputSystem = ServiceContainer.Get<InputSystem>();
            var playerController = ServiceContainer.Get<PlayerController>();
            var cameraController = ServiceContainer.Get<CameraController>();
            
            var cameraSystem = new CameraSystem(inputSystem, playerController, cameraController);
            cameraSystem.Init();
            
            return Task.FromResult((BaseSystem)cameraSystem);
        }
    }
}