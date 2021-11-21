using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public interface ITakeDamagePlayer
    {
        public int DamageMultiplier { get; set; }
        public int DemageMultiplierDefault { get; }
        public bool IsDead { get; set; }
        public Slider GetSlider { get; }
        public int MaxHealthPlayerPoints { get; }
    }
}

