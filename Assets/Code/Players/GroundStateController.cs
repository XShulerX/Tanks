namespace MVC
{
    public class GroundStateController: IPhysicsExecute
    {
        private IFlyOrGroundState _state;

        private FlyState _flyState;
        private GroundState _groundState;

        public IFlyOrGroundState State { get => _state; }

        public GroundStateController(IGamer gamer, Controllers controllers)
        {
            _flyState = new FlyState(gamer, this);
            _groundState = new GroundState();

            _state = _groundState;
            controllers.Add(this);
        }

        public void SetFlyState()
        {
            ChangeState(_flyState);
        }

        public void SetGroundState()
        {
            ChangeState(_groundState);
        }

        private void ChangeState(IFlyOrGroundState state)
        {
            _state.ExitState();
            _state = state;
            _state.EnterState();
        }

        public void PhysicsExecute()
        {
            if (_state.IsFly)
            {
                _state.OnState();
            }
        }
    }
}