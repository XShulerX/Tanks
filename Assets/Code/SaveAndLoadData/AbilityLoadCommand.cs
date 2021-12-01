using System.Collections.Generic;

namespace MVC
{
    public class AbilityLoadCommand: ILoadCommand
    {
        public bool Succeeded { get; private set; }
        private UnitStorage _unitStorage;

        public AbilityLoadCommand(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public bool Load(IMementoData mementoData)
        {

            foreach (var player in _unitStorage.Players)
            {
                foreach (var ability in player.Abilities)
                {
                    var abilityMemento = mementoData as AbilityMementoData;

                    if (player.Id == abilityMemento.playerID && ability.Key == abilityMemento.id)
                    {
                        (ability.Value as ILoadeble).Load(mementoData);
                    }
                }
            }

            Succeeded = true;
            return Succeeded;
        }
    }
}