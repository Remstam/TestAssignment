using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace TestGame
{
    public class ConfigSystemInitializer : BaseSystemInitializer<ConfigSystem>
    {
        public override Task<BaseSystem> InitAsync()
        {
            var inputConfig = Resources.Load<InputKeyboardConfig>("ScriptableObjects/InputKeyboardConfig");
            var playerConfig =
                Resources.Load<PlayerConfig>("ScriptableObjects/PlayerConfig");
            var enemySpawnerConfig = Resources.Load<EnemySpawnerConfig>("ScriptableObjects/EnemySpawnerConfig");
            var poolConfig = Resources.Load<PoolConfig>("ScriptableObjects/PoolConfig");
            var debugConfig = Resources.Load<DebugConfig>("ScriptableObjects/DebugConfig");
            var spellConfigs = Resources.LoadAll<BaseSpellConfig>("ScriptableObjects/Spells")
                .ToDictionary(x => x.Type, y => y);
            var configSystem = new ConfigSystem(inputConfig, playerConfig, enemySpawnerConfig, poolConfig, debugConfig, spellConfigs);
            
            return Task.FromResult((BaseSystem) configSystem);
        }
    }
}