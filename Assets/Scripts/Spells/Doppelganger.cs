using System;
using UnityEngine;

namespace TestGame
{
    public class Doppelganger : ITarget, IBattleMember
    {
        public event Action<Doppelganger> Died = delegate { };
        
        public event Action<IBattleMember, IBattleMember, float> DamageSent = delegate { };

        public event Action<IBattleMember> HasDied = delegate { };

        public int Priority => 1;
        public Vector3 Position => _controller.transform.position;
        public string BattleName { get; }
        public bool IsAlive() => _isAlive;

        private readonly PoolController _pool;
        private readonly DoppelgangerController _controller;
        private bool _isAlive = true;

        public Doppelganger(DoppelgangerConfig config, PoolController pool, Vector3 position, Quaternion rotation, Transform anchor)
        {
            _pool = pool;
            BattleName = config.Type.ToString();
            
            var spellInstance = pool.GetOrCreate();
            spellInstance.transform.SetParent(anchor);
            spellInstance.transform.position = position;
            spellInstance.transform.rotation = rotation;

            _controller = spellInstance.GetComponent<DoppelgangerController>();
            _controller.Init(this, config);
            _controller.OnDied += OnDied;
        }

        private void OnDied()
        {
            _isAlive = false;
            Died?.Invoke(this);
        }
        
        public void DeInit()
        {
            _controller.OnDied -= OnDied;

            _pool.Return(_controller.gameObject);
        }
        
        public void TakeDamage(string from, float damage)
        {
            if (_controller != null)
            {
                Debug.Log($"Doppelganger takes {damage} damage points from {from}.");
                _controller.TakeDamage(damage);
            }
        }
    }
}