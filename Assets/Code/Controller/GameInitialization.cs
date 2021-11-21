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
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            controllers.Add(enemyInitialization);
            var enemyList = new List<IEnemy>();
            enemyList.AddRange(enemyInitialization.GetEnemies());

            var elementsController = new ElementsController(enemyList);

            var timerController = new TimerController();
            controllers.Add(timerController);

            List<IGamer> gamerList = new List<IGamer>();
            gamerList.Add(player);
            gamerList.AddRange(enemyInitialization.GetEnemies());

            new TankDestroyingController(gamerList, timerController);
            var turnController = new TurnController(gamerList, timerController, elementsController, uiModel.StepTextField);
            controllers.Add(turnController);

            var abilityFactory = new AbilityFactory(timerController, player, box, enemyList);
            var playerAbilityController = new PlayerAbilityController(bulletPoolsInitialization.GetBullets, turnController, player, abilityFactory, abilitiesData);
            controllers.Add(playerAbilityController);

            var uiAdapter = new UIAdapter(playerAbilityController.Abilities);
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel, uiAdapter.GetAbilities()));
            controllers.Add(uiStateController);

            controllers.Add(new EnemyFireController(player.transform, enemyInitialization.GetEnemies()));
            controllers.Add(new PlayerTargetController(enemyInitialization.GetEnemies(), player));
            controllers.Add(new TakeDamageController(gamerList, elementsController));

        }
    }
}