using System;
using UnityEngine;

namespace MVC
{
    public interface ITakeDamage
    {
        public float CurrentHealthPoints { get; set; }
        public Elements TankElement { get; set; }

        public AliveStateController AliveStateController { get; }
        public GroundStateController GroundStateController { get; }

        event Action<Collision, ITakeDamage> OnCollisionEnterChange;
    }
}