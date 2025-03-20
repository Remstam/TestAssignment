using System.Collections.Generic;

namespace TestGame
{
    public class ConfigSystem : BaseSystem
    {
        private readonly PoolConfig _poolConfig;
        public IInputKeys InputKeys { get; }
        public PlayerConfig PlayerConfig { get; }
        public EnemySpawnerConfig EnemySpawnerConfig { get; }
        public PoolConfig PoolConfig { get; }
        public DebugConfig DebugConfig { get; }
        public Dictionary<SpellType, BaseSpellConfig> SpellConfigs { get; }

        //TODO: ctor is heavy, has to be some key access service
        public ConfigSystem(IInputKeys inputKeys, PlayerConfig playerConfig, EnemySpawnerConfig enemySpawnerConfig,
            PoolConfig poolConfig, DebugConfig debugConfig, Dictionary<SpellType, BaseSpellConfig> spellConfigs)
        {
            InputKeys = inputKeys;
            PlayerConfig = playerConfig;
            EnemySpawnerConfig = enemySpawnerConfig;
            PoolConfig = poolConfig;
            DebugConfig = debugConfig;
            SpellConfigs = spellConfigs;
        }

        public override void Dispose()
        {
            
        }
    }
}