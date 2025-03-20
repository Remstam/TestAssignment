using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class BulletSpell : ISpell
    {
        public SpellType Type => SpellType.Bullet;
        
        private readonly BulletSpellConfig _config;
        private readonly PlayerSystem _playerSystem;
        private readonly TimerSystem _timerSystem;
        private readonly PoolController _pool;
        private readonly HashSet<Bullet> _currentBullets = new();
        private readonly Transform _anchor;
        private bool _canCast = true;
        
        public BulletSpell(BaseSpellConfig config, PlayerSystem playerSystem, PoolSystem poolSystem, TimerSystem timerSystem)
        {
            _config = (BulletSpellConfig)config;
            _playerSystem = playerSystem;
            _timerSystem = timerSystem;
            _pool = CreatePool(_config, poolSystem);
            _anchor = new GameObject($"{config.Type}Anchor").transform;
        }
        
        private PoolController CreatePool(BulletSpellConfig config, PoolSystem poolSystem)
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
            
            var bullet = new Bullet(_config, _pool, _playerSystem.WeaponPosition, _playerSystem.WeaponRotation, _anchor);
            bullet.Died += OnDied;
            
            _currentBullets.Add(bullet);
            
            _canCast = false;
            _timerSystem.SetTimer(_config.Cooldown, () => _canCast = true);

            return true;
        }

        private void OnDied(Bullet bullet)
        {
            Debug.Log("Bullet died.");
            
            bullet.Died -= OnDied;
            bullet.DeInit();
            
            _currentBullets.Remove(bullet);
        }
        
        public void Dispose()
        {
            foreach (var bullet in _currentBullets)
            {
                bullet.Died -= OnDied;
                bullet.DeInit();
            }
        }
    }
}