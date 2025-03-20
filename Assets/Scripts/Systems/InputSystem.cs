using System.Collections.Generic;

namespace TestGame
{
    public class InputSystem : BaseSystem
    {
        private readonly IInputListener _inputListener;
        public IKeyboardSubscriber PlayerKeyboardSubscriber { get; }
        public IKeyboardSubscriber CameraKeyboardSubscriber { get; }
        public IKeyboardSubscriber DebugKeyboardSubscriber { get; }
        public IKeyboardSubscriber SpellsKeyboardSubscriber { get; }

        private readonly List<IKeyboardSubscriber> _keyboardSubscribers;
        
        public InputSystem(IInputListener inputListener, IInputKeys inputKeys)
        {
            _inputListener = inputListener;
            
            PlayerKeyboardSubscriber = new PlayerKeyboardSubscriber(inputKeys);
            CameraKeyboardSubscriber = new CameraKeyboardSubscriber(inputKeys);
            DebugKeyboardSubscriber = new DebugKeyboardSubscriber(inputKeys);
            SpellsKeyboardSubscriber = new SpellsKeyboardSubscriber(inputKeys);

            _keyboardSubscribers = new List<IKeyboardSubscriber>()
            {
                PlayerKeyboardSubscriber,
                CameraKeyboardSubscriber,
                DebugKeyboardSubscriber,
                SpellsKeyboardSubscriber
            };

            foreach (var subscriber in _keyboardSubscribers)
            {
                inputListener.AddKeyboardSubscriber(subscriber);
            }
        }

        public override void Dispose()
        {
            foreach (var subscriber in _keyboardSubscribers)
            {
                _inputListener.DeleteKeyboardSubscriber(subscriber);
            }
        }
    }
}