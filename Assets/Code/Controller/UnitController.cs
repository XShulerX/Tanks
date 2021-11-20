using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class UnitController
    {
        private UnitStorage _unitStorage;

        private float _forceModifer = 1;

        private const float PLAYER_MAX_HP = 100;
        private const float ENEMY_START_HP = 50;

        public UnitController(EnemyData enemyData, Player player, BulletPool bulletPool, out UnitStorage unitStorage)
        {
            var enemyFactory = new EnemyFactory(enemyData, bulletPool);
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            var enemies = new List<IEnemy>();
            enemies.AddRange(enemyInitialization.GetEnemies());

            var gamerList = new List<IGamer>();
            gamerList.Add(player);
            gamerList.AddRange(enemies);

            unitStorage = new UnitStorage(enemies, gamerList, player);
            _unitStorage = unitStorage;
        }

        public void ResetEnemies()
        {
            for (int i = 0; i < _unitStorage.enemies.Count; i++)
            {
                _unitStorage.enemies[i].Reset(_forceModifer, ENEMY_START_HP);
            }
        }

        public void ResetPlayer()
        {
            _unitStorage.player.Reset(PLAYER_MAX_HP);
        }

        public void IncreaseForceModifer()
        {
            _forceModifer += 0.1f;
        }
    }
}