using UnityEngine;
using UnityEngine.Assertions;

namespace TestGame
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "TestGame/Enemy/Characteristics Config")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private string _enemyName;
        [SerializeField] private PrefabInfo _prefabInfo;
        [SerializeField] private float _health;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attack;
        [SerializeField, Range(0.01f, 1f)] private float _defence;
        [SerializeField] private float _hitCooldown;

        [TextArea]
        [SerializeField] private string _description;

        public string EnemyName => _enemyName;
        public GameObject Prefab => _prefabInfo.Prefab;
        public PoolType PoolType => _prefabInfo.PoolType;
        public float Health => _health;
        public float MoveSpeed => _moveSpeed;
        public float Attack => _attack;
        public float Defence => _defence;
        public float HitCooldown => _hitCooldown;
        public string Description => _description;

        private void OnValidate()
        {
            Assert.IsTrue(Health > 0, "Health points have to be positive.");
            Assert.IsTrue(MoveSpeed > 0, "Move speed has to be positive.");
        }
    }
}