using System.Collections.Generic;

namespace TestGame
{
    public class DebugSystem : BaseSystem
    {
        private readonly InputSystem _inputSystem;
        private readonly DebugConfig _debugConfig;
        private readonly IDebugEnemiesSystem _enemiesSystem;

        public DebugSystem(InputSystem inputSystem, DebugConfig debugConfig, IDebugEnemiesSystem enemiesSystem)
        {
            _inputSystem = inputSystem;
            _debugConfig = debugConfig;
            _enemiesSystem = enemiesSystem;

            _inputSystem.DebugKeyboardSubscriber.OnKeyActions += OnKeyActions;
        }

        private void OnKeyActions(List<KeyAction> actions)
        {
            foreach (var action in actions)
            {
                if (action == KeyAction.DoRandomEnemyDamage)
                {
                    _enemiesSystem.GiveRandomDamage(_debugConfig.RandomEnemyDamage);
                }
            }
        }

        public override void Dispose()
        {
            _inputSystem.DebugKeyboardSubscriber.OnKeyActions -= OnKeyActions;
        }
    }
}