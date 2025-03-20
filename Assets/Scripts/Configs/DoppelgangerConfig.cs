using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "DoppelgangerConfig", menuName = "TestGame/Spells/Doppelganger")]
    public class DoppelgangerConfig : BaseSpellConfig
    {
        [SerializeField] private PrefabInfo _prefabInfo;
        [SerializeField] private float _health;
        [SerializeField] private int _limit;

        public override SpellType Type => SpellType.Doppelganger;
        public PrefabInfo PrefabInfo => _prefabInfo;
        public float Health => _health;
        public int Limit => _limit;
    }
}