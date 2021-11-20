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

            var unitController = new UnitController(enemyData, player, bulletPool, out UnitStorage unitStorage);

            var elementsController = new ElementsController(unitStorage);

            var timerController = new TimerController();
            controllers.Add(timerController);

            var turnController = new TurnController(unitStorage, timerController, elementsController, uiModel.GamePanelModel.StepTextField);
            controllers.Add(turnController);

            //todo - сделать и проверить GameResetController(unitStorage, unitController, bulletPool, elementsController) останавливающий все апдейты в игре и
            //выполняющий ресет контроллера шагов, возвращающий все пули в пул
            //и обновляющий врагов и игрока в зависимости от условий.

            var abilityFactory = new AbilityFactory(timerController, box, unitStorage);
            var playerAbilityController = new PlayerAbilityController(bulletPool, turnController, unitStorage.player, abilityFactory, abilitiesData);
            controllers.Add(playerAbilityController);

            var gameResetManager = new GameResetManager(unitController, bulletPool, elementsController, controllers, turnController, unitStorage, playerAbilityController);

            var uiController = new UIController(uiModel, playerAbilityController.Abilities, unitStorage, gameResetManager);
            controllers.Add(uiController);

            controllers.Add(new EnemyFireController(unitStorage));
            controllers.Add(new PlayerTargetController(unitStorage));
            controllers.Add(new TakeDamageController(unitStorage, elementsController));

            new TankDestroyingController(unitStorage, timerController, gameResetManager);

        }
    }
}