using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "BulletSpellConfig", menuName = "TestGame/Spells/Bullet")]
    public class BulletSpellConfig : BaseSpellConfig
    {
        [SerializeField] private PrefabInfo _prefabInfo;
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        
        public override SpellType Type => SpellType.Bullet;
        public PrefabInfo PrefabInfo => _prefabInfo;
        public float Damage => _damage;
        public float LifeTime => _lifeTime;
        public float Speed => _speed;
    }
}