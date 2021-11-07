using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    internal sealed class EnemyFireController : IExecute
    { 
        private IEnumerable<IEnemy> _enemies;
        private readonly Transform _target;

        public EnemyFireController(Transform target, IEnumerable<IEnemy> enemies)
        {
            _enemies = enemies;
            _target = target;
        }

        public void Execute(float deltaTime)
        {
            foreach(var enemy in _enemies)
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