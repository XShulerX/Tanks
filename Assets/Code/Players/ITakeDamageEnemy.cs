using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public interface ITakeDamageEnemy : ITakeDamage
    {
        public Elements TankElement { get; set; }
    }
}
