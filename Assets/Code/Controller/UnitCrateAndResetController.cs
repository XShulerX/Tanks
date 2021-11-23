﻿using System.Collections.Generic;

namespace MVC
{
    public class UnitCrateAndResetController: IResetable
    {
        private UnitStorage _unitStorage;

        private float _forceModifer = 1;

        public UnitCrateAndResetController(EnemyData enemyData, Player player, BulletPool bulletPool, out UnitStorage unitStorage)
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

        private void ResetEnemies()
        {
            for (int i = 0; i < _unitStorage.enemies.Count; i++)
            {
                _unitStorage.enemies[i].Reset(_forceModifer);
            }
        }

        private void ResetPlayer()
        {
            _unitStorage.player.Reset();
        }

        public void Reset()
        {
            ResetEnemies();
            ResetPlayer();
        }

        public void IncreaseForceModifer()
        {
            _forceModifer += 0.1f;
        }
    }
}