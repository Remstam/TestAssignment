namespace TestGame
{
    public class CameraKeyboardSubscriber : BaseKeyboardSubscriber
    {
        public override KeyListenType ListenType => KeyListenType.Once;
        
        public CameraKeyboardSubscriber(IInputKeys inputKeys) : base(inputKeys, InputGroup.Camera)
        {
            
        }
    }
}