using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class PlayerTargetController : IInitialization, ICleanup
    {
        private UnitStorage _unitStorage;
        private readonly Player _player;

        public PlayerTargetController(UnitStorage unitStorage, Player player)
        {
            _player = player;
            _unitStorage = unitStorage;
        }

        public void Initilazation()
        {
            foreach(var enemyTarget in _unitStorage.enemies)
            {
                enemyTarget.OnMouseUpChange += MouseOnEnemy;
            }
        }

        public void Cleanup()
        {
            foreach (var enemyTarget in _unitStorage.enemies)
            {
                enemyTarget.OnMouseUpChange -= MouseOnEnemy;
            }
        }

        private void MouseOnEnemy(Vector3 enemyPosition)
        {
            _player.SwapTarget(enemyPosition);
        }
    }
}