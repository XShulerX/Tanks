using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    public class EnemyFireController : IExecute
    { 
        private List<Enemy> _enemies;
        private readonly Transform _target;

        public EnemyFireController(Transform target)
        {
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