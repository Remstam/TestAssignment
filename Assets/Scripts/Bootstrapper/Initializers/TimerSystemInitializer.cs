using System.Threading.Tasks;

namespace TestGame
{
    public class TimerSystemInitializer : BaseSystemInitializer<TimerSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var timeSystem = new TimerSystem();

            return Task.FromResult((BaseSystem) timeSystem);
        }
    }
}