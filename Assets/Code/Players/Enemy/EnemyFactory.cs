using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyData _data;
        private BulletPool _bulletPool;

        public EnemyFactory(EnemyData data, BulletPool bulletPool)
        {
            _data = data;
            _bulletPool = bulletPool;
        }

        public IEnumerable<IEnemy> CreateEnemies()
        {
            var (enemies, enemyPosition, enemyRotation) = _data.GetEnemies();
            IEnemy[] enemiesList = new IEnemy[enemies.Length];
            for (var i = 0; i < enemies.Length; i++)
            {
                enemiesList[i] = Object.Instantiate(enemies[i], enemyPosition[i], Quaternion.Euler(enemyRotation[i])).SetPool(_bulletPool);
            }
            return enemiesList;
        }
    }
}