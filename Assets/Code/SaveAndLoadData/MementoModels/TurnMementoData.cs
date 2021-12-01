using System;

namespace MVC
{
    [Serializable]
    public class TurnMementoData: IMementoData
    {
        public int turnCount;

        public TurnMementoData(int turnCount)
        {
            this.turnCount = turnCount;
        }
    }
}