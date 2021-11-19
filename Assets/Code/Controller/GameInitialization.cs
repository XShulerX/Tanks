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


            new TankDestroyingController(unitStorage, timerController);
            var turnController = new TurnController(unitStorage, timerController, elementsController, uiModel.StepTextField);
            controllers.Add(turnController);

            //todo - сделать и проверить GameResetController(unitStorage, unitController, bulletPool, elementsController) останавливающий все апдейты в игре и
            //выполняющий ресет контроллера шагов, возвращающий все пули в пул
            //и обновляющий врагов и игрока в зависимости от условий.

            var gameResetController = new GameResetManager(unitController, bulletPool, elementsController, controllers, turnController);

            var abilityFactory = new AbilityFactory(timerController, box, unitStorage);
            var playerAbilityController = new PlayerAbilityController(bulletPool, turnController, unitStorage.player, abilityFactory, abilitiesData);
            controllers.Add(playerAbilityController);

            var uiAdapter = new UIAdapter(playerAbilityController.Abilities);
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel, uiAdapter.GetAbilities()));
            controllers.Add(uiStateController);

            controllers.Add(new EnemyFireController(unitStorage));
            controllers.Add(new PlayerTargetController(unitStorage));
            controllers.Add(new TakeDamageController(unitStorage, elementsController));

        }
    }
}