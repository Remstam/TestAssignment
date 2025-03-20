using System;
using UnityEngine;

namespace TestGame
{
    public class Bullet
    {
        public event Action<Bullet> Died = delegate { };

        private readonly PoolController _pool;
        private readonly BulletSpellConfig _config;
        private readonly BulletController _bulletController;
        private readonly string _name;

        public Bullet(BulletSpellConfig config, PoolController pool, Vector3 position, Quaternion rotation, Transform anchor)
        {
            _config = config;
            _pool = pool;
            _name = config.Type.ToString();
            
            var spellInstance = pool.GetOrCreate();
            spellInstance.transform.SetParent(anchor);
            spellInstance.transform.position = position;
            spellInstance.transform.rotation = rotation;

            _bulletController = spellInstance.GetComponent<BulletController>();
            _bulletController.Init(config);
            _bulletController.OnCollided += OnCollided;
            _bulletController.OnDied += OnDied;
        }

        private void OnDied(BulletController bullet)
        {
            Died?.Invoke(this);
        }

        private void OnCollided(IBattleMemberComponent battleMemberComponent)
        {
            var battleMember = battleMemberComponent.BattleMember;
            battleMember.TakeDamage(_name, _config.Damage);
        }

        public void DeInit()
        {
            _bulletController.OnCollided -= OnCollided;
            _bulletController.OnDied -= OnDied;
            _bulletController.DeInit();

            _pool.Return(_bulletController.gameObject);
        }
    }
}