namespace MVC
{
    public class DeadState: IAliveState
    {
        private bool _isDead;
        private bool _isAlive;

        public bool IsDead { get => _isDead; }
        public bool IsAlive { get => _isAlive; }

        public DeadState()
        {
            _isDead = true;
            _isAlive = false;
        }

        public void EnterState()
        {

        }

        public void ExitState()
        {
        }
    }
}