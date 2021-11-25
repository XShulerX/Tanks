using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class EnemyFactory : IEnemyFactory
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
            var (enemies, enemiesID, enemyPosition, enemyRotation) = _data.GetEnemies();
            IEnemy[] enemiesList = new IEnemy[enemies.Length];
            for (var i = 0; i < enemies.Length; i++)
            {
                enemiesList[i] = Object.Instantiate(enemies[i], enemyPosition[i], Quaternion.Euler(enemyRotation[i])).SetPool(_bulletPool).SetID(enemiesID[i]);
            }
            return enemiesList;
        }
    }
}