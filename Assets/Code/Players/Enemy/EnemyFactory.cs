using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyData _data;

        public EnemyFactory(EnemyData data)
        {
            _data = data;
        }

        public IEnumerable<IEnemy> CreateEnemies()
        {
            var (enemies, enemyPosition, enemyRotation) = _data.GetEnemies();
            IEnemy[] enemiesList = new IEnemy[enemies.Length];
            for (var i = 0; i < enemies.Length; i++)
            {
                enemiesList[i] = Object.Instantiate(enemies[i], enemyPosition[i], Quaternion.Euler(enemyRotation[i]));
            }
            return enemiesList;
        }
    }
}