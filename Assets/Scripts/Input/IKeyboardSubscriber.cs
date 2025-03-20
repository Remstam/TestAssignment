using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public interface IKeyboardSubscriber
    {
        public event Action<List<KeyAction>> OnKeyActions; 
          
        List<KeyCode> KeyList { get; }
        KeyListenType ListenType { get; }

        void OnKeysPressed(List<KeyCode> keyList);
    }
}