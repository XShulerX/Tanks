using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyData _data;
        private BulletPool _bulletPool;
        private Controllers _controllers;

        public EnemyFactory(EnemyData data, BulletPool bulletPool, Controllers controllers)
        {
            _data = data;
            _bulletPool = bulletPool;
            _controllers = controllers;
        }

        public IEnumerable<IEnemy> CreateEnemies()
        {
            var (enemies, enemiesID, enemyPosition, enemyRotation) = _data.GetEnemies();
            IEnemy[] enemiesList = new IEnemy[enemies.Length];
            for (var i = 0; i < enemies.Length; i++)
            {
                enemiesList[i] = Object.Instantiate(enemies[i], enemyPosition[i], Quaternion.Euler(enemyRotation[i])).SetPool(_bulletPool).SetID(enemiesID[i]).SetMainController(_controllers);
            }
            return enemiesList;
        }
    }
}