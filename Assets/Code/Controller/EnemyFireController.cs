using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    internal sealed class EnemyFireController : IExecute
    {
        private UnitStorage _unitStorage;

        public EnemyFireController(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public void Execute(float deltaTime)
        {
            foreach(var enemy in _unitStorage.enemies)
            {
                if (enemy.IsYourTurn && !enemy.IsDead)
                {
                    enemy.Fire(_unitStorage.player.transform);
                    enemy.IsShoted = true;
                    enemy.IsYourTurn = false;
                }
            }
        }
    }
}