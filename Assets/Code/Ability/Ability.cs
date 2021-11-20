using System;
using UnityEngine;

namespace MVC {
    public abstract class Ability: IRechargeableAbility
    {
        public Action abilityIsEnded = delegate () { };
        
        protected int _cooldown;
        protected bool _isOnCooldown;
        protected int _cooldownTurns;
        protected BulletPool _pool;
        protected Elements _elementType;
        protected Material _material;
        private KeyCode _key;

        public bool IsOnCooldown { get => _isOnCooldown; }
        public Elements ElementType { get => _elementType; }
        public KeyCode Key { get => _key; }

        protected Ability(BulletPool pool, AbilityModel abilityModel)
        {
            _cooldown = abilityModel.Cooldown;
            _pool = pool;
            _elementType = abilityModel.Element;
            _material = abilityModel.Material;
            _key = abilityModel.Key;
        }

        public abstract void ActivateAbility();
        public abstract void ReduceCooldown();

        public void ResetAbility()
        {
            _isOnCooldown = false;
            _cooldownTurns = 0;
        }
    }
}
