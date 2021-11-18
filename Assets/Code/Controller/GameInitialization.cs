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

            //todo - сделать GameResetController(unitStorage, unitController, bulletPool, elementsController) останавливающий все апдейты в игре и
            //выполняющий ресет контроллера шагов, возвращающий все пули в пул
            //и обновляющий врагов и игрока в зависимости от условий.


            var elementsController = new ElementsController(unitStorage);

            var timerController = new TimerController();
            controllers.Add(timerController);


            new TankDestroyingController(unitStorage, timerController);
            var turnController = new TurnController(unitStorage, timerController, elementsController, uiModel.StepTextField);
            controllers.Add(turnController);

            var abilityFactory = new AbilityFactory(timerController, player, box, unitStorage);
            var playerAbilityController = new PlayerAbilityController(bulletPool, turnController, player, abilityFactory, abilitiesData);
            controllers.Add(playerAbilityController);

            List<IRechargeableAbility> abilities = new List<IRechargeableAbility>();
            abilities.AddRange(playerAbilityController.Abilities);
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel, abilities));
            controllers.Add(uiStateController);

            controllers.Add(new EnemyFireController(player.transform, unitStorage));
            controllers.Add(new PlayerTargetController(unitStorage, player));
            controllers.Add(new TakeDamageController(unitStorage, elementsController));

        }
    }
}