using System.Threading.Tasks;

namespace TestGame
{
    public class PoolSystemInitializer : BaseSystemInitializer<PoolSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var poolsController = ServiceContainer.Get<PoolsController>();

            var poolSystem = new PoolSystem(configSystem.PoolConfig, poolsController);

            return Task.FromResult((BaseSystem) poolSystem);
        }
    }
}