using System.Collections.Generic;

namespace MVC
{
    public class PlayerAbilityController : IExecute, IResetable
    {
        private Dictionary<int, Ability> _abilities = new Dictionary<int, Ability>();
        private Player _player;
        private TurnController _turnController;
        private bool _isAbilityUsed;
        private InputController _inputController;

        public Dictionary<int, Ability> Abilities => _abilities;

        public PlayerAbilityController(BulletPool bulletPool, TurnController turnController, Player player, AbilityFactory abilityFactory, AbilitiesData abilitiesData, InputController inputController)
        {
            _turnController = turnController;
            _turnController.endGlobalTurn += ReduceAbilitiesCooldown;
            _player = player;

            var abilities = abilitiesData.AbilitiesModel;

            for (int i = 0; i < abilities.Count; i++)
            {
                var ability = abilityFactory.Create<Ability>(bulletPool, abilities[i]);
                ability.abilityIsEnded += EnemyTurn;
                _abilities.Add(abilities[i].AbilitiID, ability);
            }
            _inputController = inputController;
            _inputController.AbilityKeyIsPressed += UseAbility;
        }

        private void ReduceAbilitiesCooldown()
        {
            foreach (var ability in _abilities)
            {
                if (ability.Value.IsOnCooldown)
                {
                    ability.Value.ReduceCooldown();
                }
            }
        }

        public void Execute(float deltaTime)
        {
            if (!_player.IsYourTurn || _isAbilityUsed)
            {
                return;
            }
            _inputController.CheckAbilityKey();
        }

        private void UseAbility(int idAbility)
        {
            if (_abilities[idAbility].IsOnCooldown) return;

            switch (_abilities[idAbility].ElementType)
            {
                case Elements.Water:
                    if (!_player.TryGetTarget(out IEnemy target)) return;
                    else if (target.AliveStateController.State.IsAlive)
                    {
                        ActivateAbility(idAbility);
                    }
                    break;
                case Elements.Fire:
                case Elements.Terra:
                    ActivateAbility(idAbility);
                    break;
            }
        }

        private void ActivateAbility(int idAbility)
        {
            _player.IsShoted = true;
            _isAbilityUsed = true;
            _abilities[idAbility].ActivateAbility();
        }

        public void EnemyTurn()
        {
            _player.IsYourTurn = false;
            _isAbilityUsed = false;
            _player.IsShoted = false;
        }

        public void Reset()
        {
            foreach (var ability in _abilities)
            {
                ability.Value.ResetAbility();
            }
        }
    }
}

