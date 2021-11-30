using System.Collections.Generic;

namespace MVC
{
    public class UnitCrateAndResetController: IResetable
    {
        private UnitStorage _unitStorage;
        private bool _isPlayerWin;

        private float _forceModifer = 1;

        public UnitStorage UnitStorage { get => _unitStorage; }
        public float ForceModifer { get => _forceModifer; }

        public UnitCrateAndResetController(EnemyData enemyData, List<Player> players, BulletPool bulletPool, Controllers controllers, out UnitStorage unitStorage)
        {
            var enemyFactory = new EnemyFactory(enemyData, bulletPool, controllers);
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            var enemies = new List<IEnemy>();
            enemies.AddRange(enemyInitialization.GetEnemies());

            var gamerList = new List<IGamer>();
            gamerList.AddRange(players);
            gamerList.AddRange(enemies);

            unitStorage = new UnitStorage(enemies, gamerList, players);
            _unitStorage = unitStorage;
        }

        private void ResetEnemies()
        {
            for (int i = 0; i < _unitStorage.Enemies.Count; i++)
            {
                _unitStorage.Enemies[i].Reset(_forceModifer, _isPlayerWin);
            }
        }

        private void ResetPlayer()
        {
            foreach (var player in _unitStorage.Players)
            {
                player.Reset();
            }        
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

        public void SetStageStatus(bool isPlayerWin)
        {
            _isPlayerWin = isPlayerWin;
        }

        public void SetForceModifier(float modifier)
        {
            _forceModifer = modifier;
        }
    }
}