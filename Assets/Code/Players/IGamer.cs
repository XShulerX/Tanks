using System;

namespace MVC
{
    public interface IGamer : ITakeDamage
    {
        public Action wasKilled { get; set; }
        bool IsYourTurn { get; set; }
        public bool IsDead { get; set; }
    }
}