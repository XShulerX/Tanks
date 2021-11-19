using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class PlayerAbilityController : IExecute
    {
        private readonly Dictionary<int, Ability> _abilities = new Dictionary<int, Ability>();
        private Player _player;
        private TurnController _turnController;
        private bool _isAbilityUsed;
        private InputKeyAbilityController _inputImplementation;

        public Dictionary<int, Ability> Abilities => _abilities;

        public PlayerAbilityController(BulletPool bulletPool, TurnController turnController, Player player, AbilityFactory abilityFactory, AbilitiesData abilitiesData)
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

            var inputAdapter = new InputAdapter(abilities);
            _inputImplementation = new InputKeyAbilityController(inputAdapter.GetMatching());
            _inputImplementation.AbilityKeyIsPressed += UseAbility;
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
            _inputImplementation.CheckKey();
        }

        private void UseAbility(int idAbility)
        {
            if (_abilities[idAbility].IsOnCooldown) return;
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
    }
}

