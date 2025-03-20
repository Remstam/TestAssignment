namespace TestGame
{
    public class DebugKeyboardSubscriber : BaseKeyboardSubscriber
    {
        public override KeyListenType ListenType => KeyListenType.Once;

        public DebugKeyboardSubscriber(IInputKeys inputKeys) : base(inputKeys, InputGroup.Debug)
        {
        }
    }
}