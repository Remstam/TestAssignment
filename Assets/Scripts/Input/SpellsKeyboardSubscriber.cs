namespace TestGame
{
    public class SpellsKeyboardSubscriber : BaseKeyboardSubscriber
    {
        public override KeyListenType ListenType => KeyListenType.Once;

        public SpellsKeyboardSubscriber(IInputKeys inputKeys) : base(inputKeys, InputGroup.Spell)
        {
            
        }
    }
}