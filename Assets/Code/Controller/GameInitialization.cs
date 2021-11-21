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
            var bulletPool = bulletPoolsInitialization.GetBullets;
            controllers.Add(bulletPool);

            var unitController = new UnitController(enemyData, player, bulletPool, out UnitStorage unitStorage);
            controllers.Add(unitController);

            var gameResetManager = new GameResetManager(unitController, controllers);

            var elementsController = new ElementsController(unitStorage);
            controllers.Add(elementsController);

            var timerController = new TimerController();
            controllers.Add(timerController);

            var turnController = new TurnController(unitStorage, timerController, elementsController, uiModel.GamePanelModel.StepTextField);
            controllers.Add(turnController);

            var abilityFactory = new AbilityFactory(timerController, box, unitStorage);
            var playerAbilityController = new PlayerAbilityController(bulletPool, turnController, unitStorage.player, abilityFactory, abilitiesData);
            controllers.Add(playerAbilityController);

            var uiController = new UIController(uiModel, playerAbilityController.Abilities, gameResetManager);
            controllers.Add(uiController);

            controllers.Add(new EnemyFireController(unitStorage));
            controllers.Add(new PlayerTargetController(unitStorage));
            controllers.Add(new TakeDamageController(unitStorage, elementsController));

            new TankDestroyingController(unitStorage, timerController, gameResetManager);

        }
    }
}