using System;
using System.Collections.Generic;

namespace TestGame
{
    public class SpellsSystem : BaseSystem
    {
        public event Action<ISpell> SpellChanged = delegate { };
        public event Action<ISpell> SpellCasted = delegate { };

        public ISpell CurrentSpell => _currentSpell;
        
        private readonly List<ISpell> _spells;
        private readonly InputSystem _inputSystem;

        private ISpell _currentSpell;
        private int _currentSpellIndex;
        
        public SpellsSystem(List<ISpell> spells, InputSystem inputSystem)
        {
            _spells = spells;
            _inputSystem = inputSystem;

            _inputSystem.SpellsKeyboardSubscriber.OnKeyActions += OnKeyActions;
            _currentSpell = spells[_currentSpellIndex];
        }

        private void OnKeyActions(List<KeyAction> actions)
        {
            foreach (var action in actions)
            {
                if (action == KeyAction.CastSpell)
                {
                    var wasCasted = _currentSpell.Cast();
                    if (wasCasted)
                    {
                        SpellCasted?.Invoke(_currentSpell);
                    }
                }

                if (action == KeyAction.NextSpell)
                {
                    _currentSpell = _spells.GetNext(ref _currentSpellIndex);
                    SpellChanged?.Invoke(_currentSpell);
                }

                if (action == KeyAction.PrevSpell)
                {
                    _currentSpell = _spells.GetPrev(ref _currentSpellIndex);
                    SpellChanged?.Invoke(_currentSpell);
                }
            }
        }

        public override void Dispose()
        {
            _inputSystem.SpellsKeyboardSubscriber.OnKeyActions -= OnKeyActions;
        }
    }
}