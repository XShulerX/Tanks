using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class PlayerAbilityController : IExecute
    {
        private readonly List<Ability> _abilities = new List<Ability>();
        private Player _player;
        private TurnController _turnController;
        private bool _isAbilityUsed;

        public List<Ability> Abilities => _abilities;

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
                _abilities.Add(ability);
            }
        }

        private void ReduceAbilitiesCooldown()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                if(_abilities[i].IsOnCooldown)
                {
                    _abilities[i].ReduceCooldown();
                }
            }
        }

        public void Execute(float deltaTime)
        {
            if (!_player.IsYourTurn || _isAbilityUsed)
            {
                return;
            }

            for (int i = 0; i < _abilities.Count; i++)
            {
                if (Input.GetKeyDown(_abilities[i].Key))
                {
                    if (_abilities[i].IsOnCooldown) return;
                    UseAbility(_abilities[i]);
                }
            }
        }

        private void UseAbility(Ability ability)
        {
            _player.IsShoted = true;
            _isAbilityUsed = true;
            ability.ActivateAbility();
        }

        public void EnemyTurn()
        {
            _player.IsYourTurn = false;
            _isAbilityUsed = false;
            _player.IsShoted = false;
        }
    }
}

