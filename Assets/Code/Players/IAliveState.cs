namespace MVC
{
    public interface IAliveState
    {
        public bool IsDead { get; }
        public bool IsAlive { get; }
        public void EnterState();
        public void ExitState();

    }
}