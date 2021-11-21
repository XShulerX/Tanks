using UnityEngine;

namespace MVC {
    public class WaterAbility : Ability
    {
        public Player player;
        public WaterAbility(BulletPool pool, AbilityModel abilityModel) : base(pool, abilityModel)
        {
        }
        public override void ActivateAbility()
        {
            var element = _pool.GetFreeElement();
            element.transform.position = player.GetGun.position;
            element.transform.rotation = player.GetGun.rotation;
            element.GetComponent<MeshRenderer>().material = _material;
            element.GetComponent<Rigidbody>().AddForce(player.GetGun.forward * 40, ForceMode.Impulse);
            var bulletEntity = element.GetComponent<Bullet>();
            bulletEntity.element = Elements.Water;
            bulletEntity.SetContainer(_pool.GetContainer);
            bulletEntity.InvokeTimer();

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