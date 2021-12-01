using System;
using System.Collections.Generic;

namespace MVC
{
    public class PlayerAbilityController : IExecute, IResetable
    {
        private UnitStorage _unitStorage;
        private TurnController _turnController;
        private bool _isAbilityUsed;
        private InputController _inputController;
        private Player _activePlayer;

        public Player ActivePlayer { get => _activePlayer; }

        public PlayerAbilityController(BulletPool bulletPool, TurnController turnController, UnitStorage unitStorage, AbilityFactory abilityFactory, AbilitiesData abilitiesData, InputController inputController)
        {
            _turnController = turnController;
            _turnController.endGlobalTurn += ReduceAbilitiesCooldown;
            _turnController.changeActivePlayer += ChangeActivePlayer;

            _unitStorage = unitStorage;

            var abilities = abilitiesData.AbilitiesModel;

            foreach (var player in _unitStorage.Players)
            {
                for (int i = 0; i < abilities.Count; i++)
                {
                    var ability = abilityFactory.Create<Ability>(bulletPool, abilities[i], player);
                    ability.abilityIsEnded += NextTurn;
                    player.Abilities.Add(abilities[i].AbilitiID, ability);
                }
            }

            _activePlayer = _unitStorage.Players[0];

            _inputController = inputController;
            _inputController.AbilityKeyIsPressed += UseAbility;
        }

        private void ChangeActivePlayer(Player player)
        {
            _activePlayer = player;
        }

        private void ReduceAbilitiesCooldown()
        {
            foreach (var player in _unitStorage.Players)
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
                    ActivateAbility(idAbility);
                    break;
                case Elements.Terra:

                    var enemy = _unitStorage.Enemies.Find(enemy => enemy.GroundStateController.State.IsOnGround && enemy.AliveStateController.State.IsAlive);
                    if (enemy is null) return;

                    ActivateAbility(idAbility);

                    break;
            }
        }

        private void ActivateAbility(int idAbility)
        {
            _isAbilityUsed = true;
            _activePlayer.Abilities[idAbility].ActivateAbility();
        }

        public void NextTurn()
        {
            if (_activePlayer.TryGetTarget(out IEnemy target))
            {
                _activePlayer.SetTargetAsNull();
            }
            _activePlayer.IsShoted = true;
            _activePlayer.IsYourTurn = false;
            _activePlayer.CircleOfChoice.SetActive(false);
            _isAbilityUsed = false;
        }

        public void Reset()
        {
            foreach (var player in _unitStorage.Players)
            {
                foreach (var ability in player.Abilities)
                {
                    ability.Value.ResetAbility();
                }
            }
        }
    }
}

