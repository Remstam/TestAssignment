using System;

namespace TestGame
{
    public interface ISpell : IDisposable
    {
        public SpellType Type { get; }

        bool Cast();
    }
}