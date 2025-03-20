using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public abstract class BaseKeyboardSubscriber : IKeyboardSubscriber
    {
        public event Action<List<KeyAction>> OnKeyActions;
        public List<KeyCode> KeyList { get; }
        public abstract KeyListenType ListenType { get; }

        private readonly List<KeyInfo> _keyInfo;

        protected BaseKeyboardSubscriber(IInputKeys inputKeys, InputGroup inputGroup)
        {
            _keyInfo = inputKeys.GetInputKeys(inputGroup);
            KeyList = _keyInfo.Select(x => x.KeyCode).ToList();
        }
        
        public void OnKeysPressed(List<KeyCode> keyList)
        {
            var actions = _keyInfo
                .Where(x => keyList.Contains(x.KeyCode))
                .Select(x => x.KeyAction)
                .ToList();

            if (actions.Count == 0)
            {
                return;
            }
            
            OnKeyActions?.Invoke(actions);
        }
    }
}