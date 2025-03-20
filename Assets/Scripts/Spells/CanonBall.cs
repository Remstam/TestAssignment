using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class CanonBall
    {
        public event Action<CanonBall> Died = delegate { };

        public Vector3 Position => _canonBallController.transform.position;
        public string Name => _name;
        
        private readonly PoolController _pool;
        private readonly CanonBallSpellConfig _config;
        private readonly HashSet<IBattleMember> _activeTargets = new();
        private readonly CanonBallController _canonBallController;
        private readonly string _name;

        public CanonBall(CanonBallSpellConfig config, PoolController pool, Vector3 position, Quaternion rotation, Transform anchor)
        {
            _config = config;
            _pool = pool;
            _name = config.Type.ToString();
            
            var spellInstance = pool.GetOrCreate();
            spellInstance.transform.SetParent(anchor);
            spellInstance.transform.position = position;
            spellInstance.transform.rotation = rotation;

            _canonBallController = spellInstance.GetComponent<CanonBallController>();
            _canonBallController.Init(config);
            _canonBallController.OnCollided += OnCollided;
            _canonBallController.OnExploded += OnExploded;
        }

        private void OnExploded(CanonBallController ball)
        {
            foreach (var target in _activeTargets)
            {
                target.TakeDamage(_name, _config.Damage);
            }
            
            Died?.Invoke(this);
        }

        private void OnCollided(IBattleMemberComponent battleMemberComponent, bool isActive)
        {
            // TODO: can add some juicy logic there too;
            return;
            
            var battleMember = battleMemberComponent.BattleMember;
            
            if (isActive)
            {
                _activeTargets.Add(battleMember);
            }
            else
            {
                _activeTargets.Remove(battleMember);
            }
        }

        public void DeInit()
        {
            _activeTargets.Clear();
            
            _canonBallController.OnCollided -= OnCollided;
            _canonBallController.OnExploded -= OnExploded;
            _canonBallController.DeInit();

            _pool.Return(_canonBallController.gameObject);
        }
    }
}