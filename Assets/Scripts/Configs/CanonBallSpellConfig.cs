using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "CanonBallSpellConfig", menuName = "TestGame/Spells/Canon Ball")]
    public class CanonBallSpellConfig : BaseSpellConfig
    {
        [SerializeField] private PrefabInfo _prefabInfo;
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        [SerializeField] private float _damageRadius;

        public override SpellType Type => SpellType.CanonBall;
        public PrefabInfo PrefabInfo => _prefabInfo;
        public float Damage => _damage;
        public float LifeTime => _lifeTime;
        public float Speed => _speed;
        public float DamageRadius => _damageRadius;
    }
}