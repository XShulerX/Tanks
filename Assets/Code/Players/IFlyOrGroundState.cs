namespace MVC
{
    public interface IFlyOrGroundState
    {
        public bool IsFly { get; }
        public bool IsOnGround { get; }
        public void EnterState();
        public void ExitState();
        public void OnState();
    }
}