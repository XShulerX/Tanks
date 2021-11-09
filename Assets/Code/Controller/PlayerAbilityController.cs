using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class PlayerAbilityController : IExecute
    {
        private List<Ability> _abilities = new List<Ability>();
        private Player _player;
        private TurnController _turnController;
        private bool _isAbilityUsed;

        private const int WATER = 0;
        private const int FIRE = 1;
        private const int TERRA = 2;

        public PlayerAbilityController(List<BulletPool> bulletPools, TimerController timerController, GameObject box, TurnController turnController, Player player)
        {
            _turnController = turnController;
            _turnController.endGlobalTurn += ReduceAbilitiesCooldown;
            _player = player;

            var waterAbility = new WaterAbility(2, timerController, bulletPools[WATER], box);
            waterAbility.abilityIsEnded += EnemyTurn;
            _abilities.Add(waterAbility);

            var fireAbility = new FireAbility(1, timerController, bulletPools[FIRE], _player);
            fireAbility.abilityIsEnded += EnemyTurn;
            _abilities.Add(fireAbility);

            var terraAbility = new TerraAbility(2, timerController, bulletPools[TERRA], _player);
            terraAbility.abilityIsEnded += EnemyTurn;
            _abilities.Add(terraAbility);
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_abilities[WATER].IsOnCooldown) return;
                UseAbility(_abilities[WATER]);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_abilities[FIRE].IsOnCooldown) return;
                UseAbility(_abilities[FIRE]);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_abilities[TERRA].IsOnCooldown) return;
                UseAbility(_abilities[TERRA]);
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

