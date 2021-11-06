using System.Collections.Generic;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, Player player)
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
            controllers.Add(new TurnController(gamerList, timerController));
            controllers.Add(new EnemyFireController(player.transform, enemyInitialization.GetEnemies()));
            controllers.Add(new PlayerFireController(player));
            controllers.Add(new PlayerTargetController(enemyInitialization.GetEnemies(), player));
            controllers.Add(new TakeDamageController(gamerList));
            controllers.Add(new FireTargetAbilityController(bulletPoolsInitialization.GetBullets, timerController, poolModel));
        }
    }
}