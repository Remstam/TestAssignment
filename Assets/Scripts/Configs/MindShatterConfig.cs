using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "MindShatterConfig", menuName = "TestGame/Spells/MindShatter")]
    public class MindShatterConfig : BaseSpellConfig
    {
        [SerializeField, Min(1)] private int _pointsCount;
        public override SpellType Type => SpellType.MindShatter;
        public int PointsCount => _pointsCount;
    }
}