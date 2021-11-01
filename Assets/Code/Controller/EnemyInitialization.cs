using System.Collections.Generic;

namespace MVC
{
    internal sealed class EnemyInitialization : IInitialization
    {
        private readonly IEnemyFactory _enemyFactory;
        private List<IEnemy> _enemies;

        public EnemyInitialization(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            var enemies = _enemyFactory.CreateEnemies();
            _enemies = new List<IEnemy>();
            _enemies.AddRange(enemies);
        }

        public void Initilazation()
        {
        }

        public IEnumerable<IEnemy> GetEnemies()
        {
            foreach(var enemy in _enemies)
            {
                yield return enemy;
            }
        }

    }
}