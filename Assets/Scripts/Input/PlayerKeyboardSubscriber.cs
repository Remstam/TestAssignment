namespace TestGame
{
    public class PlayerKeyboardSubscriber : BaseKeyboardSubscriber
    {
        public override KeyListenType ListenType => KeyListenType.Constant;

        public PlayerKeyboardSubscriber(IInputKeys inputKeys) : base(inputKeys, InputGroup.Player)
        {
            
        }
    }
}