using System.Collections.Generic;

namespace MVC
{
    public class AbilityLoadCommand
    {
        public bool Succeeded { get; private set; }
        private PlayerAbilityController _playerAbilityController;

        public AbilityLoadCommand(PlayerAbilityController playerAbilityController)
        {
            _playerAbilityController = playerAbilityController;
        }

        public bool Load(List<AbilityMementoData> abilityMementoDatas)
        {
            foreach(var ability in _playerAbilityController.Abilities)
            {
                for (int i = 0; i < abilityMementoDatas.Count; i++)
                {
                    if(ability.Key == abilityMementoDatas[i].id)
                    {
                        (ability.Value as ILoadeble).Load<AbilityMementoData>(abilityMementoDatas[i]);
                    }
                }
            }
            Succeeded = true;
            return Succeeded;
        }
    }
}