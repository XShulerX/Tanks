using System;

namespace MVC
{
    public class AliveStateController
    {
        private IAliveState _state;
        private IGamer _gamer;

        private DeadState _deadState;
        private AliveState _aliveState;

        public IAliveState State { get => _state; }

        public AliveStateController(IGamer gamer)
        {
            _gamer = gamer;
            _deadState = new DeadState();
            _aliveState = new AliveState();

            _state = _aliveState;

            _gamer.wasKilled += SetDeadState;
        }

        public void SetDeadState(IGamer gamer)
        {
            ChangeState(_deadState);
        }
        public void SetAliveState()
        {
            ChangeState(_aliveState);
        }

        private void ChangeState(IAliveState state)
        {
            _state.ExitState();
            _state = state;
            _state.EnterState();
        }
    }
}