using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, Player player, GameObject box, UIInitializationModel uiModel, AbilitiesData abilitiesData)
        {
            var poolModel = new PoolModel();
            var bulletPoolsInitialization = new BulletPoolsInitialization(poolModel);

            var enemyFactory = new EnemyFactory(enemyData, bulletPoolsInitialization.GetBullets);

            var timerController = new TimerController();
            controllers.Add(timerController);

            
            var levelController = new LevelController(controllers, timerController, uiModel.StepTextField, enemyFactory, player, box, bulletPoolsInitialization, uiModel, abilitiesData);
            controllers.Add(levelController);

        }
    }
}