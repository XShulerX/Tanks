using System;

namespace MVC
{
    [Serializable]
    public class TurnMementoData: IMementoData
    {
        public int turnCount;
        public int shootedOrDeadEnemies;

        public TurnMementoData(int turnCount, int shootedOrDeadEnemies)
        {
            this.turnCount = turnCount;
            this.shootedOrDeadEnemies = shootedOrDeadEnemies;
        }
    }
}