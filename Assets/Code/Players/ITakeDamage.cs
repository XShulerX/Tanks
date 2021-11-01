using System;
using UnityEngine;

namespace MVC
{
    public interface ITakeDamage
    {
        public int CurrentHealthPoints { get; set; }

        event Action<Collision, ITakeDamage> OnCollisionEnterChange;
    }
}