using System.Collections.Generic;

namespace MVC
{
    public class UnitStorage
    {
        private List<IEnemy> _enemies;
        private List<IGamer> _gamers;
        private List<Player> _players;

        public List<IEnemy> Enemies { get => _enemies; }
        public List<IGamer> Gamers { get => _gamers; }
        public List<Player> Players { get => _players; set => _players = value; }

        public UnitStorage(List<IEnemy> enemiesList, List<IGamer> gamerList, List<Player> players)
        {
            _enemies = new List<IEnemy>(enemiesList);
            _gamers = new List<IGamer>(gamerList);
            _players = new List<Player>(players);
        }
    }
}