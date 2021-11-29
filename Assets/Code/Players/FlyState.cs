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
        private const float TWITCH_AMPLITUDE = 0.05f;

        public FlyState(IGamer gamer)
        {
            _isFly = true;
            _isOnGround = false;
            _gamer = gamer;
        }

        public void EnterState()
        {
            _gamer.transform.Translate(Vector3.up * 2);
        }

        public void ExitState()
        {
            _gamer.transform.Translate(Vector3.down * 2);
        }

        public void OnState()
        {
            _gamer.transform.position = new Vector3(_gamer.transform.position.x, _gamer.transform.position.y + Mathf.Sin(Time.fixedTime * TWITCH_SPEED) * TWITCH_AMPLITUDE, _gamer.transform.position.z);
        }
    }
}