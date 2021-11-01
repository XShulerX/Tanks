using System.Collections.Generic;

namespace MVC
{
    public interface IEnemyFactory
    {
        IEnumerable<IEnemy> CreateEnemies();
    }
}