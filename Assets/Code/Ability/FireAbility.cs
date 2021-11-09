using System.Collections.Generic;
using UnityEngine;

namespace MVC {
    public class FireAbility : Ability
    {
        private Player _player;
        public FireAbility(int cooldown, TimerController timerController, BulletPool pool, Player player) : base(cooldown, timerController, pool)
        {
            _player = player;
            _cooldown = cooldown;
            _timerController = timerController;
            _pool = pool;
        }
        public override void ActivateAbility()
        {
            if (_pool.HasFreeElement(out var element))
            {
                element.transform.position = _player.GetGun.position;
                element.transform.rotation = _player.GetGun.rotation;
                element.GetComponent<Rigidbody>().AddForce(_player.GetGun.forward * 40, ForceMode.Impulse);
                var bulletEntity = element.GetComponent<Bullet>();
                bulletEntity.element = Elements.Terra;
                bulletEntity.SetContainer(_pool.GetContainer);
                bulletEntity.InvokeTimer();
            }
            abilityIsEnded.Invoke();
            _isOnCooldown = true;
        }

        public override void ReduceCooldown()
        {
            _cooldownTurns++;            
            if (_cooldownTurns == _cooldown)
            {
                _isOnCooldown = false;
                _cooldownTurns = 0;
            }
        }
    }
}