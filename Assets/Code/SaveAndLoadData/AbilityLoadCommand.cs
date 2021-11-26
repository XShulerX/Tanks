using System.Collections.Generic;

namespace MVC
{
    public class AbilityLoadCommand: ILoadCommand
    {
        public bool Succeeded { get; private set; }
        private PlayerAbilityController _playerAbilityController;

        public AbilityLoadCommand(PlayerAbilityController playerAbilityController)
        {
            _playerAbilityController = playerAbilityController;
        }

        public bool Load(IMementoData mementoData)
        {
            
            foreach(var ability in _playerAbilityController.Abilities)
            {
                if(ability.Key == (mementoData as AbilityMementoData).id)
                {
                    (ability.Value as ILoadeble).Load(mementoData);
                }
            }
            Succeeded = true;
            return Succeeded;
        }
    }
}