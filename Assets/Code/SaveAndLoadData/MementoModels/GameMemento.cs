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
        public TurnMementoData turnMemento;
        public StageMementoData stageMemento;

        public GameMemento(List<EnemyMementoData> enemiesMementos, PlayerMementoData playerMemento, List<AbilityMementoData> abilitiesMemento, TurnMementoData turnMemento, StageMementoData stageMemento)
        {
            this.enemiesMementos = enemiesMementos;
            this.playerMemento = playerMemento;
            this.abilitiesMemento = abilitiesMemento;
            this.turnMemento = turnMemento;
            this.stageMemento = stageMemento;
        }
    }
}