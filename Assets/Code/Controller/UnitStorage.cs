using System;
using System.Collections.Generic;

namespace MVC
{
    public class UnitStorage
    {
        public Action gamersListUpdated = delegate () { };

        public readonly List<IEnemy> enemies;
        public readonly List<IGamer> gamers;
        public Player player;

        public UnitStorage(List<IEnemy> enemiesList, List<IGamer> gamerList, Player player)
        {
            enemies = new List<IEnemy>(enemiesList);
            gamers = new List<IGamer>(gamerList);
            this.player = player;
        }
    }
}