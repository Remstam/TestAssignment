using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public class InputListener : MonoBehaviour, IInputListener
    {
        private static readonly List<KeyCode> KeysPressed = new(10);
        private static readonly Dictionary<KeyListenType, Func<KeyCode, bool>> InputFuncs =
            new()
            {
                {KeyListenType.Once, Input.GetKeyDown},
                {KeyListenType.Constant, Input.GetKey}
            };

        private readonly HashSet<IKeyboardSubscriber> _keyboardSubscribers = new(10);

        public void AddKeyboardSubscriber(IKeyboardSubscriber keyboardSubscriber)
        {
            _keyboardSubscribers.Add(keyboardSubscriber);
        }

        public void DeleteKeyboardSubscriber(IKeyboardSubscriber keyboardSubscriber)
        {
            _keyboardSubscribers.Remove(keyboardSubscriber);
        }
          
        public void Update()
        {
            if (Input.anyKeyDown)
            {
                ProcessKeyPresses(_keyboardSubscribers, KeyListenType.Once);
            }

            if (Input.anyKey)
            {
                ProcessKeyPresses(_keyboardSubscribers, KeyListenType.Constant);
            }
        }

        private void ProcessKeyPresses(HashSet<IKeyboardSubscriber> subscribers, KeyListenType keyListenType)
        {
            var subs = subscribers.Where(x => x.ListenType == keyListenType);
            foreach (var sub in subs)
            {
                KeysPressed.Clear();

                var inputFunc = InputFuncs[keyListenType];
                foreach (var key in sub.KeyList.Where(inputFunc))
                {
                    KeysPressed.Add(key);
                }
                         
                sub.OnKeysPressed(KeysPressed);
            }
        }
    }
}