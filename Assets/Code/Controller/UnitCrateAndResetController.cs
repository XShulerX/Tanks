using System.Collections.Generic;

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

            var gamerList = new List<IGamer>();
            gamerList.Add(player);
            gamerList.AddRange(enemyInitialization.GetEnemies());

            unitStorage = new UnitStorage(enemyInitialization.GetEnemies(), gamerList, player);
            _unitStorage = unitStorage;
        }

        private void ResetEnemies()
        {
            for (int i = 0; i < _unitStorage.Enemies.Count; i++)
            {
                _unitStorage.Enemies[i].Reset(_forceModifer);
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