using System;

namespace MVC
{
    [Serializable]
    public class AbilityMementoData: IMementoData
    {
        public int playerID;
        public int id;
        public bool isOnCooldown;
        public int cooldownTurns;

        public AbilityMementoData(int playerID, int id, bool isOnCooldown, int cooldownTurns)
        {
            this.playerID = playerID;
            this.id = id;
            this.isOnCooldown = isOnCooldown;
            this.cooldownTurns = cooldownTurns;
        }
    }
}