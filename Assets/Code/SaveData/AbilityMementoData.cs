using System;

namespace MVC
{
    [Serializable]
    public class AbilityMementoData: IMomentoData
    {
        public int id;
        public bool isOnCooldown;
        public int cooldownTurns;

        public AbilityMementoData(int id, bool isOnCooldown, int cooldownTurns)
        {
            this.id = id;
            this.isOnCooldown = isOnCooldown;
            this.cooldownTurns = cooldownTurns;
        }
    }
}