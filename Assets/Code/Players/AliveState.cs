namespace MVC
{
    public class AliveState: IAliveState
    {
        private bool _isDead;
        private bool _isAlive;

        public bool IsDead { get => _isDead; }
        public bool IsAlive { get => _isAlive; }

        public AliveState()
        {
            _isDead = false;
            _isAlive = true;
        }

        public void EnterState()
        {
        }

        public void ExitState()
        {
        }
    }
}