using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public interface ITakeDamageEnemy : ITakeDamage
    {
        public Elements TankElement { get; set; }
        public Slider GetSlider { get; }
        public int MaxHealthEnemyPoints { get; }
        public int GetDefaultEnemyHP { get; }
    }
}
