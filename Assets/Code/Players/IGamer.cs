using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public interface IGamer : ITakeDamage, ILoadeble
    {
        bool IsYourTurn { get; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        Action<IGamer> wasKilled { get; set; }
        public Material Material { get; set; }
        public Transform Turret { get; set; }

        public GameObject GetTankObject { get; }
        public GameObject GetWrackObject { get; }
        public ParticleSystem GetParticleExplosion { get; }

        public Image GamerIconElement { get;}
    }
}