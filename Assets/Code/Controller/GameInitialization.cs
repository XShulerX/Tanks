using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, List<Player> players, GameObject box, UIInitializationModel uiModel, AbilitiesData abilitiesData)
        {
            foreach (var player in players)
            {
                player.Init(controllers);
            }


            var poolModel = new PoolModel();
            var bulletPoolsInitialization = new BulletPoolsInitialization(poolModel);
            var bulletPool = bulletPoolsInitialization.GetBullets;
            controllers.Add(bulletPool);

            var unitController = new UnitCrateAndResetController(enemyData, players, bulletPool, controllers, out UnitStorage unitStorage);
            controllers.Add(unitController);

            var gameResetManager = new GameResetOrEndManager(unitController, controllers);

            var elementsController = new ElementsController(unitStorage);
            controllers.Add(elementsController);

            var timerController = new TimerController();
            controllers.Add(timerController);

            var turnController = new TurnController(unitStorage, timerController, elementsController, uiModel.GamePanelModel.StepTextField);
            controllers.Add(turnController);

            var abilityFactory = new AbilityFactory(timerController, box, unitStorage);

            var inputAdapter = new InputAdapter(abilitiesData.AbilitiesModel);
            var inputController = new InputController(inputAdapter.GetMatching());
            controllers.Add(inputController);

            var playerAbilityController = new PlayerAbilityController(bulletPool, turnController, unitStorage, abilityFactory, abilitiesData, inputController);
            controllers.Add(playerAbilityController);

            controllers.Add(new EnemyFireController(unitStorage));
            controllers.Add(new PlayerTargetController(unitStorage));
            controllers.Add(new TakeDamageController(unitStorage, elementsController));

            new TankDestroyingController(unitStorage, timerController, gameResetManager);
            new FlyEnemiesManager(unitStorage.Enemies, turnController, gameResetManager);

            var momentoSaver = new MementosSaver(unitStorage, gameResetManager, turnController);
            var loadComandManager = new LoadCommandManager(gameResetManager, playerAbilityController, turnController, unitStorage, timerController);
            new SaveDataController(inputController, momentoSaver, loadComandManager);

            var uiController = new UIController(uiModel, playerAbilityController, gameResetManager, loadComandManager, inputController);
            controllers.Add(uiController);
        }
    }
}