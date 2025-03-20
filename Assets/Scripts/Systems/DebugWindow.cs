using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class DebugWindow : BaseSystem
    {
        private readonly DebugWindowController _controller;
        private readonly PlayerSystem _playerSystem;
        private readonly SpellsSystem _spellsSystem;
        private readonly Dictionary<SpellType, BaseSpellConfig> _spellConfigs;
        private readonly Dictionary<SpellType, DateTime> _lastSpellCasts = new();
        private readonly YieldInstruction _wait = new WaitForSeconds(0.02f);

        private SpellType CurrentSpell => _spellsSystem.CurrentSpell.Type;
        
        public DebugWindow(DebugWindowController controller, PlayerSystem playerSystem, SpellsSystem spellsSystem, ConfigSystem configSystem)
        {
            _controller = controller;
            _playerSystem = playerSystem;
            _spellsSystem = spellsSystem;
            _spellConfigs = configSystem.SpellConfigs;

            foreach (SpellType type in Enum.GetValues(typeof(SpellType)))
            {
                _lastSpellCasts.Add(type, DateTime.MinValue);
            }
        }

        public void Init()
        {
            _controller.UpdateHealth(_playerSystem.Health);
            _controller.UpdateCurrentSpell(CurrentSpell.ToString());
            _controller.UpdateSpellCooldown(GetCooldown(CurrentSpell));

            _playerSystem.TookDamage += OnPlayerTookDamage;
            _spellsSystem.SpellCasted += OnSpellCasted;
            _spellsSystem.SpellChanged += OnSpellChanged;

            _controller.StartCoroutine(UpdateCooldown());
        }

        private void OnSpellChanged(ISpell spell)
        {
            _controller.UpdateCurrentSpell(spell.Type.ToString());
        }

        private IEnumerator UpdateCooldown()
        {
            while (true)
            {
                _controller.UpdateSpellCooldown(GetCooldown(CurrentSpell));
                yield return _wait;
            }
        }
        
        private double GetCooldown(SpellType currentSpell)
        {
            var durationSpan = TimeSpan.FromSeconds(_spellConfigs[currentSpell].Cooldown);
            var timePassed = DateTime.Now - _lastSpellCasts[CurrentSpell];
            var coolDown = durationSpan - timePassed;
            if (coolDown.TotalMilliseconds < 0)
            {
                coolDown = TimeSpan.Zero;
            }

            return coolDown.TotalMilliseconds;
        }
        
        private void OnPlayerTookDamage()
        {
            _controller.UpdateHealth(_playerSystem.Health);
        }

        private void OnSpellCasted(ISpell spell)
        {
            _lastSpellCasts[spell.Type] = DateTime.Now;
        }
        
        public override void Dispose()
        {
            _playerSystem.TookDamage -= OnPlayerTookDamage;
            _spellsSystem.SpellCasted -= OnSpellCasted;
            _spellsSystem.SpellChanged -= OnSpellChanged;
        }
    }
}