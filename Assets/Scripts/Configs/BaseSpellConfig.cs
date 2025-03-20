using UnityEngine;

namespace TestGame
{
    public abstract class BaseSpellConfig : ScriptableObject
    {
        [SerializeField] private float _cooldown = 1f;

        public abstract SpellType Type { get; }
        public float Cooldown => _cooldown;
    }
}