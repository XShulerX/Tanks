using System;

namespace MVC
{
    [Serializable]
    public class StageMementoData: IMementoData
    {
        public int attemptsCount;
        public float forceModifer;
        public int stageCount;

        public StageMementoData(int attemptsCount, float forceModifer, int stageCount)
        {
            this.attemptsCount = attemptsCount;
            this.forceModifer = forceModifer;
            this.stageCount = stageCount;
        }
    }
}