namespace TestGame
{
    public interface IInputListener
    {
        void AddKeyboardSubscriber(IKeyboardSubscriber keyboardSubscriber);
        void DeleteKeyboardSubscriber(IKeyboardSubscriber keyboardSubscriber);
    }
}