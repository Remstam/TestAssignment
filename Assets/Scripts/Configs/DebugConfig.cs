using UnityEngine;
using UnityEngine.Assertions;

namespace TestGame
{
    [CreateAssetMenu(fileName = "DebugConfig", menuName = "TestGame/Input/Debug Config")]
    public class DebugConfig : ScriptableObject
    {
        [SerializeField, Min(1f)] private float _randomEnemyDamage;

        public float RandomEnemyDamage => _randomEnemyDamage;

        private void OnValidate()
        {
            Assert.IsTrue(_randomEnemyDamage > 0f, "Damage points have to be positive.");
        }
    }
}