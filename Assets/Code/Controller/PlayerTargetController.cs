using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class PlayerTargetController : IInitialization, ICleanup
    {
        private readonly IEnumerable<IPlayerTarget> _enemyTargets;
        private readonly Player _player;

        public PlayerTargetController(IEnumerable<IPlayerTarget> enemyTargets, Player player)
        {
            _player = player;
            _enemyTargets = enemyTargets;
        }

        public void Initilazation()
        {
            foreach(var enemyTarget in _enemyTargets)
            {
                enemyTarget.OnMouseUpChange += MouseOnEnemy;
            }
        }

        public void Cleanup()
        {
            foreach (var enemyTarget in _enemyTargets)
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