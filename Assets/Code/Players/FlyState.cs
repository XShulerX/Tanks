using UnityEngine;

namespace MVC
{
    public class FlyState: IFlyOrGroundState
    {
        private bool _isFly;
        private bool _isOnGround;

        private IGamer _gamer;
        public bool IsFly { get => _isFly; }
        public bool IsOnGround { get => _isOnGround; }

        private const float TWITCH_SPEED = 3f;
        private const float TWITCH_AMPLITUDE = 0.03f;

        public FlyState(IGamer gamer)
        {
            _isFly = true;
            _isOnGround = false;
            _gamer = gamer;
        }

        public void EnterState()
        {
            _gamer.GetTankObject.transform.Translate(Vector3.up * 1.5f);
        }

        public void ExitState()
        {
            _gamer.GetTankObject.transform.position = _gamer.transform.position;
        }

        public void OnState()
        {
            _gamer.GetTankObject.transform.position = new Vector3(_gamer.GetTankObject.transform.position.x, _gamer.GetTankObject.transform.position.y + Mathf.Sin(Time.fixedTime * TWITCH_SPEED) * TWITCH_AMPLITUDE, _gamer.GetTankObject.transform.position.z);
        }
    }
}