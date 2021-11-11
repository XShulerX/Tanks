using System;

namespace MVC {
    public abstract class Ability: IRechargeableAbility
    {
        public Action abilityIsEnded = delegate () { };
        
        protected int _cooldown;
        protected bool _isOnCooldown;
        protected int _cooldownTurns;
        protected BulletPool _pool;
        private Elements _elementType;

        public bool IsOnCooldown { get => _isOnCooldown; }
        public Elements ElementType { get => _elementType; }

        protected Ability(int cooldown, BulletPool pool, Elements element)
        {
            _cooldown = cooldown;
            _pool = pool;
            _elementType = element;
        }

        public abstract void ActivateAbility();
        public abstract void ReduceCooldown();
    }
}
