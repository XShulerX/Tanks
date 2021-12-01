using System;
using System.Collections.Generic;

namespace MVC
{
    [Serializable]
    public class GameMemento
    {
        public List<EnemyMementoData> enemiesMementos;
        public List<PlayerMementoData> playersMementos;
        public List<AbilityMementoData> abilitiesMemento;
        public TurnMementoData turnMemento;
        public StageMementoData stageMemento;

        public GameMemento(List<EnemyMementoData> enemiesMementos, List<PlayerMementoData> playersMementos, List<AbilityMementoData> abilitiesMemento, TurnMementoData turnMemento, StageMementoData stageMemento)
        {
            this.enemiesMementos = enemiesMementos;
            this.playersMementos = playersMementos;
            this.abilitiesMemento = abilitiesMemento;
            this.turnMemento = turnMemento;
            this.stageMemento = stageMemento;
        }
    }
}