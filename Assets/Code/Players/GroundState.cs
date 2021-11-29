namespace MVC
{
    public class GroundState : IFlyOrGroundState
    {
        private bool _isFly;
        private bool _isOnGround;

        public bool IsFly { get => _isFly; }
        public bool IsOnGround { get => _isOnGround; }

        public GroundState()
        {
            _isFly = false;
            _isOnGround = true;
        }

        public void EnterState()
        {
        }

        public void ExitState()
        {
        }

        public void OnState()
        {
        }
    }
}