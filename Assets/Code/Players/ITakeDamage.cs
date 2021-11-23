using System;
using UnityEngine;

namespace MVC
{
    public interface ITakeDamage
    {
        public float CurrentHealthPoints { get; set; }
        public Elements TankElement { get; set; }

        event Action<Collision, ITakeDamage> OnCollisionEnterChange;
    }
}