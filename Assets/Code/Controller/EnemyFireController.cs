using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    public class EnemyFireController : IExecute
    { 
        private List<IEnemy> _enemies;
        private readonly Transform _target;

        public EnemyFireController(Transform target, List<IEnemy> enemies)
        {
            _enemies = enemies;
            _target = target;
        }

        public void Execute(float deltaTime)
        {
            foreach(var enemy in _enemies)
            {
                if (enemy.isYourTurn)
                {
                    enemy.Fire(_target);
                    enemy.isYourTurn = false;
                }
            }
        }
    }
}