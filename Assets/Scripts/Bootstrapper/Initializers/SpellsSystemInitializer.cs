using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestGame
{
    public class SpellsSystemInitializer : BaseSystemInitializer<SpellsSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var configSystem = ServiceContainer.Get<ConfigSystem>();
            var playerSystem = ServiceContainer.Get<PlayerSystem>();
            var poolSystem = ServiceContainer.Get<PoolSystem>();
            var timerSystem = ServiceContainer.Get<TimerSystem>();
            var targetSystem = ServiceContainer.Get<TargetSystem>();
            var battleSystem = ServiceContainer.Get<BattleSystem>();
            var enemiesSystem = ServiceContainer.Get<EnemiesSystem>();
            var fenceSystem = ServiceContainer.Get<FenceSystem>();
            
            var spells = new List<ISpell>()
            {
                new CanonBallSpell(configSystem.SpellConfigs[SpellType.CanonBall], playerSystem, poolSystem, timerSystem, battleSystem),
                new BulletSpell(configSystem.SpellConfigs[SpellType.Bullet], playerSystem, poolSystem, timerSystem),
                new DoppelgangerSpell(configSystem.SpellConfigs[SpellType.Doppelganger], playerSystem, poolSystem, timerSystem, targetSystem, battleSystem),
                new MindShatterSpell(configSystem.SpellConfigs[SpellType.MindShatter], timerSystem, targetSystem, enemiesSystem, fenceSystem)
            };
            var inputSystem = ServiceContainer.Get<InputSystem>();
            var spellsSystem = new SpellsSystem(spells, inputSystem);

            return Task.FromResult((BaseSystem) spellsSystem);
        }
    }
}