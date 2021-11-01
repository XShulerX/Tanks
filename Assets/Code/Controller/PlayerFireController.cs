using UnityEngine;

namespace MVC
{
    internal sealed class PlayerFireController : IExecute
    {
        private readonly Player _player;

        public PlayerFireController(Player player)
        {
            _player = player;
        }

        public void Execute(float deltaTime)
        {
            if (!_player.isYourTurn)
            {
                return;
            }

            if (Input.GetButtonDown(ButtonsManager.FIRE1))
            {
                _player.Fire();
                _player.isYourTurn = false;
            }
        }
    }
}