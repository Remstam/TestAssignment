using System.Threading.Tasks;

namespace TestGame
{
    public class FenceSystemInitializer : BaseSystemInitializer<FenceSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var fenceController = ServiceContainer.Get<FenceController>();
            var fenceSystem = new FenceSystem(fenceController);
            fenceSystem.Init();

            return Task.FromResult((BaseSystem) fenceSystem);
        }
    }
}