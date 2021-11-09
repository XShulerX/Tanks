using System;

namespace MVC {
    public abstract class Ability
    {
        public Action abilityIsEnded = delegate () { };
        
        protected int _cooldown;
        protected bool _isOnCooldown;
        protected int _cooldownTurns;
        protected TimerController _timerController;
        protected BulletPool _pool;

        public bool IsOnCooldown { get => _isOnCooldown; }

        protected Ability(int cooldown, TimerController timerController, BulletPool pool)
        {
            _cooldown = cooldown;
            _timerController = timerController;
            _pool = pool;
        }

        public abstract void ActivateAbility();
        public abstract void ReduceCooldown();
    }
}
