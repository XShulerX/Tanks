using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, Player player, GameObject box, UIInitializationModel uiModel)
        {
            var enemyFactory = new EnemyFactory(enemyData);
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            controllers.Add(enemyInitialization);
            var enemyList = new List<IEnemy>();
            enemyList.AddRange(enemyInitialization.GetEnemies());

            var poolModel = new PoolModel();
            var bulletPoolsInitialization = new BulletPoolsInitialization(poolModel);
            var elementsController = new ElementsController(enemyList);

            var timerController = new TimerController();
            controllers.Add(timerController);

            List<IGamer> gamerList = new List<IGamer>();
            gamerList.Add(player);
            gamerList.AddRange(enemyInitialization.GetEnemies());

            var turnController = new TurnController(gamerList, timerController, elementsController, uiModel.StepTextField);
            controllers.Add(turnController);

            var playerAbilityController = new PlayerAbilityController(bulletPoolsInitialization.GetBullets, timerController, box, turnController, player, enemyList);
            controllers.Add(playerAbilityController);

            List<IRechargeableAbility> abilities = new List<IRechargeableAbility>();
            abilities.AddRange(playerAbilityController.Abilities);
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel, abilities));
            controllers.Add(uiStateController);

            controllers.Add(new EnemyFireController(player.transform, enemyInitialization.GetEnemies()));
            controllers.Add(new PlayerTargetController(enemyInitialization.GetEnemies(), player));
            controllers.Add(new TakeDamageController(gamerList, elementsController));

        }
    }
}