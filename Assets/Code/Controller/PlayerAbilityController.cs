using System;
using System.Collections.Generic;

namespace MVC
{
    public class PlayerAbilityController : IExecute, IResetable
    {
        private List<Player> _players;
        private TurnController _turnController;
        private bool _isAbilityUsed;
        private InputController _inputController;
        private Player _activePlayer;

        public Player ActivePlayer { get => _activePlayer; }

        public PlayerAbilityController(BulletPool bulletPool, TurnController turnController, List<Player> players, AbilityFactory abilityFactory, AbilitiesData abilitiesData, InputController inputController)
        {
            _turnController = turnController;
            _turnController.endGlobalTurn += ReduceAbilitiesCooldown;
            _turnController.changeActivePlayer += ChangeActivePlayer;

            _players = players;

            var abilities = abilitiesData.AbilitiesModel;

            foreach (var player in players)
            {
                for (int i = 0; i < abilities.Count; i++)
                {
                    var ability = abilityFactory.Create<Ability>(bulletPool, abilities[i], player);
                    ability.abilityIsEnded += NextTurn;
                    player.Abilities.Add(abilities[i].AbilitiID, ability);
                }
            }

            _activePlayer = players[0];

            _inputController = inputController;
            _inputController.AbilityKeyIsPressed += UseAbility;
        }

        private void ChangeActivePlayer(Player player)
        {
            _activePlayer = player;
        }

        private void ReduceAbilitiesCooldown()
        {
            foreach (var player in _players)
            {
                foreach (var ability in player.Abilities)
                {
                    if (ability.Value.IsOnCooldown)
                    {
                        ability.Value.ReduceCooldown();
                    }
                }
            }
 
        }

        public void Execute(float deltaTime)
        {
            if (!_activePlayer.IsYourTurn || _isAbilityUsed)
            {
                return;
            }
            _inputController.CheckAbilityKey();
        }

        private void UseAbility(int idAbility)
        {
            if (_activePlayer.Abilities[idAbility].IsOnCooldown) return;

            switch (_activePlayer.Abilities[idAbility].ElementType)
            {
                case Elements.Water:
                    if (!_activePlayer.TryGetTarget(out IEnemy target)) return;
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
            _activePlayer.IsShoted = true;
            _isAbilityUsed = true;
            _activePlayer.Abilities[idAbility].ActivateAbility();
        }

        public void NextTurn()
        {
            _activePlayer.IsYourTurn = false;
            _isAbilityUsed = false;
        }

        public void Reset()
        {
            foreach (var player in _players)
            {
                foreach (var ability in player.Abilities)
                {
                    ability.Value.ResetAbility();
                }
            }
        }
    }
}

