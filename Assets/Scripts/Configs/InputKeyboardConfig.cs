using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public interface IInputKeys
    {
        List<KeyInfo> GetInputKeys(InputGroup group);
    }

    [CreateAssetMenu(fileName = "InputKeyboardConfig", menuName = "TestGame/Input/Keyboard Config")]
    public class InputKeyboardConfig : ScriptableObject, IInputKeys
    {
        [SerializeField] private List<KeyInfo> _keys = new List<KeyInfo>()
        {
            new(InputGroup.Player, KeyAction.MoveLeft, KeyCode.LeftArrow),
            new(InputGroup.Player, KeyAction.MoveRight, KeyCode.RightArrow),
            new(InputGroup.Player, KeyAction.MoveUp, KeyCode.UpArrow),
            new(InputGroup.Player, KeyAction.MoveDown, KeyCode.DownArrow),
            new(InputGroup.Spell, KeyAction.CastSpell, KeyCode.X),
            new(InputGroup.Spell, KeyAction.PrevSpell, KeyCode.Q),
            new(InputGroup.Spell, KeyAction.NextSpell, KeyCode.W),
            new(InputGroup.Camera, KeyAction.SwitchCamera, KeyCode.C),
            new(InputGroup.Debug, KeyAction.DoRandomEnemyDamage, KeyCode.R)
        };

        public List<KeyInfo> GetInputKeys(InputGroup group)
        {
            return _keys.Where(x => x.InputGroup == group).ToList();
        }
    }
}