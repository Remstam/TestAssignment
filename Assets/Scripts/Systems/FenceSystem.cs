namespace TestGame
{
    public class FenceSystem : BaseSystem
    {
        public MoveBorders MoveBorders { get; private set; }

        private readonly FenceController _fenceController;

        public FenceSystem(FenceController fenceController)
        {
            _fenceController = fenceController;
        }

        public void Init()
        {
            MoveBorders = GetMoveBorders();
        }

        // TODO: no fency crap, ok?
        // It seems there is some better way to calc fence borders
        private MoveBorders GetMoveBorders()
        {
            var fences = _fenceController.FenceSides;
            var left = fences[0].position.x + fences[0].localScale.x / 2f;
            var right = fences[1].position.x - fences[1].localScale.x / 2f;
            var up = fences[2].position.z - fences[2].localScale.x / 2f;
            var down = fences[3].position.z + fences[3].localScale.x / 2f;
            
            return new MoveBorders()
            {
                left = left,
                right = right,
                up = up,
                down = down
            };
        }
        
        public override void Dispose()
        {
            
        }
    }
}