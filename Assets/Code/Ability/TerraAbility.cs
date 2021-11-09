using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TerraAbility : Ability
    {
        private Player _player;
        private int _shots;
        private List<IEnemy> _enemies = new List<IEnemy>();

        private const int NUMBER_OF_BULLETS = 1;
        private const float TIME_OF_ACTIVATION_BULLET = 1f;
        public TerraAbility(int cooldown, TimerController timerController, BulletPool pool, Player player, List<IEnemy> enemies) : base(cooldown, timerController, pool)
        {
            _enemies.AddRange(enemies);
            _player = player;
            _cooldown = cooldown;
            _timerController = timerController;
            _pool = pool;
        }

        public override void ActivateAbility()
        {        
            for (int j = 0; j < NUMBER_OF_BULLETS; j++)
            {
                var timer = new TimeData(TIME_OF_ACTIVATION_BULLET + j * 2);
                timer.OnTimerEndWithBool += TerraShot;
                _timerController.AddTimer(timer);
            }
        }

        public void TerraShot()
        {
            if (_pool.HasFreeElement(out var element))
            {
                _player.SwapTarget(GetRandomEnemy().position);
                element.transform.position = _player.GetGun.position;
                element.transform.rotation = _player.GetGun.rotation;
                element.GetComponent<Rigidbody>().AddForce(_player.GetGun.forward * 40, ForceMode.Impulse);
                var bulletEntity = element.GetComponent<Bullet>();
                bulletEntity.element = Elements.Terra;
                bulletEntity.SetContainer(_pool.GetContainer);
                bulletEntity.InvokeTimer();
                _shots++;
            }

            if (_shots == NUMBER_OF_BULLETS)
            {
                abilityIsEnded.Invoke();
                _isOnCooldown = true;
            }
        }

        private Transform GetRandomEnemy()
        {
            var playerTarget = Random.Range(0, _enemies.Count - 1);

            return _enemies[playerTarget].transform;
        }


        public override void ReduceCooldown()
        {
            _cooldownTurns++;
            if (_cooldownTurns == _cooldown)
            {
                _shots = 0;
                _isOnCooldown = false;
                _cooldownTurns = 0;
            }
        }
    }
}