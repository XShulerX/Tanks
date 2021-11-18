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
                _unitStorage.enemies[i].CurrentHealthPoints = ENEMY_START_HP * _forceModifer;
                _unitStorage.enemies[i].SetDamageModifer(_forceModifer);
                _unitStorage.enemies[i].GetWrackObject.SetActive(false);
                _unitStorage.enemies[i].GetTankObject.SetActive(true);
                _unitStorage.enemies[i].IsDead = false;
                _unitStorage.enemies[i].IsShoted = false;
                _unitStorage.enemies[i].IsYourTurn = false;
            }
        }

        public void ResetPlayer()
        {
            _unitStorage.player.CurrentHealthPoints = PLAYER_MAX_HP;
            _unitStorage.player.GetWrackObject.SetActive(false);
            _unitStorage.player.GetTankObject.SetActive(true);
            _unitStorage.player.IsDead = false;
            _unitStorage.player.IsShoted = false;
            _unitStorage.player.IsYourTurn = true;
        }

        public void IncreaseForceModifer()
        {
            _forceModifer += 0.1f;
        }
    }
}