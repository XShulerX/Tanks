using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, Player player, UnityEngine.GameObject box, Text text, GameObject abilityPanel)
        {
            var poolModel = new PoolModel();
            var bulletPoolsInitialization = new BulletPoolsInitialization(poolModel);

            var timerController = new TimerController();
            controllers.Add(timerController);
            var enemyFactory = new EnemyFactory(enemyData);
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            List<IGamer> gamerList = new List<IGamer>();
            gamerList.Add(player);
            gamerList.AddRange(enemyInitialization.GetEnemies());
            controllers.Add(enemyInitialization);
            var enemyList = new List<IEnemy>();
            enemyList.AddRange(enemyInitialization.GetEnemies());
            var elementsController = new ElementsController(enemyList);
            var turnController = new TurnController(gamerList, timerController, elementsController, text);
            controllers.Add(turnController);
            controllers.Add(new EnemyFireController(player.transform, enemyInitialization.GetEnemies()));
            controllers.Add(new PlayerFireController(player));
            controllers.Add(new PlayerTargetController(enemyInitialization.GetEnemies(), player));
            controllers.Add(new TakeDamageController(gamerList, elementsController));
            controllers.Add(new FireTargetAbilityController(bulletPoolsInitialization.GetBullets, timerController, poolModel, box, turnController, abilityPanel));
        }
    }
}