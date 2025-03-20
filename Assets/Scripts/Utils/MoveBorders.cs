namespace TestGame
{
    public struct MoveBorders
    {
        public float left, right, up, down;

        public override string ToString()
        {
            return $"{left}, {right}, {up}, {down}";
        }
    }
}