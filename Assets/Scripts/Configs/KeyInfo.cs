using System;
using UnityEngine;

namespace TestGame
{
    [Serializable]
    public class KeyInfo
    {
        public InputGroup InputGroup;
        public KeyAction KeyAction;
        public KeyCode KeyCode;

        public KeyInfo(InputGroup inputGroup, KeyAction keyAction, KeyCode keyCode)
        {
            InputGroup = inputGroup;
            KeyAction = keyAction;
            KeyCode = keyCode;
        }
    }
}