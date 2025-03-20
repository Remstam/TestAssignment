using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame
{
    public class DebugWindowController : MonoBehaviour
    {
        [SerializeField] private Text _health;
        [SerializeField] private Text _currentSpell;
        [SerializeField] private Text _spellCooldown;

        public void UpdateHealth(float health)
        {
            _health.text = $"Health: {health.ToString(CultureInfo.InvariantCulture)}";
        }

        public void UpdateCurrentSpell(string currentSpell)
        {
            _currentSpell.text = $"Spell: {currentSpell}";
        }

        public void UpdateSpellCooldown(double cooldown)
        {
            _spellCooldown.text = $"Cooldown: {cooldown.ToString("#.000", CultureInfo.InvariantCulture)}";
        }
    }
}