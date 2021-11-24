using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class LoadPlayerAbilityController : ICommand
    {
        public bool Succeeded { get; private set; }
        private PlayerAbilityController _playerAbilityController;

        public LoadPlayerAbilityController(PlayerAbilityController playerAbilityController)
        {
            _playerAbilityController = playerAbilityController;
        }

        public bool Load(GameMemento savedData)
        {
            for (int i = 0; i < _playerAbilityController.Abilities.Count; i++)
            {
                _playerAbilityController.Abilities[i].IsOnCooldown = savedData.abilitiesMemento[i].isOnCooldown;
                _playerAbilityController.Abilities[i].CooldownTurns = savedData.abilitiesMemento[i].cooldownTurns;
            }
            Succeeded = true;
            return Succeeded;
        }
    }
}

