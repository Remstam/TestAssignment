using System.Threading.Tasks;

namespace TestGame
{
    public class InputSystemInitializer : BaseSystemInitializer<InputSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var inputListener = ServiceContainer.Get<IInputListener>();
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var inputSystem = new InputSystem(inputListener, configSystem.InputKeys);
            
            return Task.FromResult((BaseSystem)inputSystem);
        }
    }
}