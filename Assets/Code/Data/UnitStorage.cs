using System.Collections.Generic;

namespace MVC
{
    public class UnitStorage
    {
        private List<IEnemy> _enemies;
        private List<IGamer> _gamers;
        public Player player;

        public List<IEnemy> Enemies { get => _enemies; }
        public List<IGamer> Gamers { get => _gamers; }

        public UnitStorage(IEnumerable<IEnemy> enemiesList, List<IGamer> gamerList, Player player)
        {
            _enemies = new List<IEnemy>(enemiesList);
            _gamers = new List<IGamer>(gamerList);
            this.player = player;
        }
    }
}