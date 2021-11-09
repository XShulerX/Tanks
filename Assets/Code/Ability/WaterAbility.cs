using UnityEngine;

namespace MVC {
    public class WaterAbility : Ability
    {
        private Player _player;
        public WaterAbility(int cooldown, BulletPool pool, Player player) : base(cooldown, pool)
        {
            _player = player;
            _cooldown = cooldown;
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