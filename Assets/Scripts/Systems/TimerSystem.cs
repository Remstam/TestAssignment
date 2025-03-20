using System;
using System.Threading.Tasks;

namespace TestGame
{
    public class TimerSystem : BaseSystem
    {
        // TODO: obviously it has to be done better than that
        public async void SetTimer(float seconds, Action onTime)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            onTime?.Invoke();
        }
        
        public override void Dispose()
        {
            
        }
    }
}