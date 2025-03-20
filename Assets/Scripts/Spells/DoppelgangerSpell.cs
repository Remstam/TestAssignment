using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class DoppelgangerSpell : ISpell
    {
        public SpellType Type => SpellType.Doppelganger;
        
        private readonly DoppelgangerConfig _config;
        private readonly PlayerSystem _playerSystem;
        private readonly TimerSystem _timerSystem;
        private readonly TargetSystem _targetSystem;
        private readonly BattleSystem _battleSystem;
        private readonly PoolController _pool;
        private readonly HashSet<Doppelganger> _currentDoppels = new();
        private readonly Transform _anchor;
        private bool _canCast = true;

        public DoppelgangerSpell(BaseSpellConfig config, PlayerSystem playerSystem, PoolSystem poolSystem,
            TimerSystem timerSystem, TargetSystem targetSystem, BattleSystem battleSystem)
        {
            _config = (DoppelgangerConfig)config;
            _playerSystem = playerSystem;
            _timerSystem = timerSystem;
            _targetSystem = targetSystem;
            _battleSystem = battleSystem;
            _pool = CreatePool(_config, poolSystem);
            _anchor = new GameObject($"{config.Type}Anchor").transform;
        }
        
        private PoolController CreatePool(DoppelgangerConfig config, PoolSystem poolSystem)
        {
            var pool = poolSystem.CreatePool(config.Type.ToString(), config.PrefabInfo.PoolType, config.PrefabInfo.Prefab);
            return pool;
        }
        
        public bool Cast()
        {
            if (!_canCast)
            {
                return false;
            }

            if (_currentDoppels.Count >= _config.Limit)
            {
                return false;
            }
            
            var doppelganger = new Doppelganger(_config, _pool, _playerSystem.Position, _playerSystem.Rotation, _anchor);
            doppelganger.Died += OnDied;
            
            _currentDoppels.Add(doppelganger);
            _targetSystem.RegisterTarget(doppelganger);
            _battleSystem.AddBattleMember(doppelganger);
            
            _canCast = false;
            _timerSystem.SetTimer(_config.Cooldown, () => _canCast = true);

            return true;
        }

        private void OnDied(Doppelganger doppel)
        {
            Debug.Log("Doppelganger died.");
            
            doppel.Died -= OnDied;
            doppel.DeInit();
            
            _currentDoppels.Remove(doppel);
            _targetSystem.UnregisterTarget(doppel);
            _battleSystem.RemoveBattleMember(doppel);
        }

        public void Dispose()
        {
            foreach (var doppel in _currentDoppels)
            {
                doppel.Died -= OnDied;
                doppel.DeInit();
            }
        }
    }
}