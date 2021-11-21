using System.Collections.Generic;

namespace MVC
{
    public class UIAdapter
    {
        private List<IRechargeableAbility> _abilities;

        public UIAdapter(Dictionary<int, Ability> abilities)
        {
            _abilities = new List<IRechargeableAbility>();

            foreach (var ability in abilities)
            {
                _abilities.Add(ability.Value as IRechargeableAbility);
            }
        }

        public List<IRechargeableAbility> GetAbilities()
        {
            return _abilities;
        }
    }
}