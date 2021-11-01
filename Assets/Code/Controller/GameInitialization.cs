using System.Collections.Generic;

namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, EnemyData enemyData, Player player)
        {
            var enemyFactory = new EnemyFactory(enemyData);
            var enemyInitialization = new EnemyInitialization(enemyFactory);
            List<IPlayerTurn> gamerList = new List<IPlayerTurn>();
            gamerList.Add(player);
            gamerList.AddRange(enemyInitialization.GetEnemies()); 
            controllers.Add(enemyInitialization);
            controllers.Add(new TurnController(gamerList.ToArray(), 60f));
            controllers.Add(new EnemyFireController(player.transform, enemyInitialization.GetEnemies()));
        }
    }
}