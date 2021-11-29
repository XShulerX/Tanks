using UnityEngine;

namespace MVC
{
    internal sealed class PlayerTargetController : IInitialization, ICleanup
    {
        private UnitStorage _unitStorage;

        public PlayerTargetController(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public void Initilazation()
        {
            foreach(var enemyTarget in _unitStorage.Enemies)
            {
                enemyTarget.OnMouseUpChange += MouseOnEnemy;
            }
        }

        public void Cleanup()
        {
            foreach (var enemyTarget in _unitStorage.Enemies)
            {
                enemyTarget.OnMouseUpChange -= MouseOnEnemy;
            }
        }

        private void MouseOnEnemy(IEnemy enemy)
        {
            _unitStorage.player.SwapTarget(enemy);
        }
    }
}