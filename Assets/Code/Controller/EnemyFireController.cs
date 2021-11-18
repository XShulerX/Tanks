using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    internal sealed class EnemyFireController : IExecute
    {
        private UnitStorage _unitStorage;
        private readonly Transform _target;

        public EnemyFireController(Transform target, UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
            _target = target;
        }

        public void Execute(float deltaTime)
        {
            foreach(var enemy in _unitStorage.enemies)
            {
                if (enemy.IsYourTurn && !enemy.IsDead)
                {
                    enemy.Fire(_target);
                    enemy.IsShoted = true;
                    enemy.IsYourTurn = false;
                }
            }
        }
    }
}