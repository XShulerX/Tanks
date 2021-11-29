using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal class FlyEnemiesManager
    {
        private List<IEnemy> _enemies;
        private TurnController _turnController;
        private int _maxFlyableEnemiesCount;

        public FlyEnemiesManager(List<IEnemy> enemies, TurnController turnController)
        {
            _enemies = enemies;
            _turnController = turnController;

            _turnController.endGlobalTurn += RecalculateFlyableTanks;

            RecalculateFlyableTanks();
        }

        private void RecalculateFlyableTanks()
        {
            SetGroundState();

            CalcMaxFlyableEnemies();

            if (_maxFlyableEnemiesCount == 0) return;
            
            var flyingEnemiesCount = 0;
            var aliveEnemies = new List<IEnemy>();
            foreach (var enemy in _enemies)
            {
                if (enemy.AliveStateController.State.IsDead) continue;

                aliveEnemies.Add(enemy);
                if (Random.Range(0, 1) == 1)
                {
                    flyingEnemiesCount++;
                    enemy.GroundStateController.SetFlyState();

                    if (flyingEnemiesCount == _maxFlyableEnemiesCount) return;
                }
            }
            
            if (flyingEnemiesCount == 0 && aliveEnemies.Count > 0)
            {
                aliveEnemies[Random.Range(0, _enemies.Count - 1)].GroundStateController.SetFlyState();
            }

        }

        private void CalcMaxFlyableEnemies()
        {
            var aliveEnemiesCount = 0;
            foreach (var enemy in _enemies)
            {
                if (enemy.AliveStateController.State.IsAlive)
                {
                    aliveEnemiesCount++;
                }

                _maxFlyableEnemiesCount = aliveEnemiesCount - 1;
            }
        }

        private void SetGroundState()
        {
            foreach (var enemy in _enemies)
            {
                enemy.GroundStateController.SetGroundState();
            }
        }
    }
}