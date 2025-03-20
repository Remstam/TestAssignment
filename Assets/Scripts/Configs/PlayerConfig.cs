using UnityEngine;
using UnityEngine.Assertions;

namespace TestGame
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "TestGame/Player/Characteristics Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _health;
        [SerializeField] private float _attack;
        [SerializeField, Range(0.01f, 1f)] private float _defence;
        [SerializeField] private float _hitCooldown;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float Health => _health;
        public float Attack => _attack;
        public float Defence => _defence;
        public float HitCooldown => _hitCooldown;

        private void OnValidate()
        {
            Assert.IsTrue(MoveSpeed > 0, "Player's move speed is non-positive");
            Assert.IsTrue(RotationSpeed > 0, "Player's rotation is non-positive");
            Assert.IsTrue(Health > 0, "Player's health is non-positive");
            Assert.IsTrue(Attack > 0, "Player's attack is non-positive");
        }
    }
}