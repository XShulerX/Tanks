using System;

namespace MVC
{
    public interface IGamer : ITakeDamage
    {
        bool IsYourTurn { get; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        Action<IGamer> wasKilled { get; set; }
    }
}