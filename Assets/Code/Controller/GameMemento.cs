using System;
using System.Collections.Generic;

namespace MVC
{
    [Serializable]
    public class GameMemento
    {
        public List<EnemyMementoData> enemiesMementos;
        public PlayerMementoData playerMemento;
        public List<AbilityMementoData> abilitiesMemento;
        public int turnCount;
        public int stageCount;
        public int attemptsCount;
        public float forceModifier;

        public GameMemento(List<EnemyMementoData> enemiesMementos, PlayerMementoData playerMemento, List<AbilityMementoData> abilitiesMemento, int turnCount, int stageCount, int attemptsCount, float forceModifier)
        {
            this.enemiesMementos = enemiesMementos;
            this.playerMemento = playerMemento;
            this.abilitiesMemento = abilitiesMemento;
            this.turnCount = turnCount;
            this.stageCount = stageCount;
            this.attemptsCount = attemptsCount;
            this.forceModifier = forceModifier;
        }
    }
}