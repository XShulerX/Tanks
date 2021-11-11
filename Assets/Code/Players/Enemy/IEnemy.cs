using System;
using UnityEngine;

namespace MVC
{
    public interface IEnemy : IGamer, IPlayerTarget
    {
        void Fire(Transform target);

        public Elements TankElement { get; set; }

        public Transform Turret { get; set; }

        public Transform transform { get; }
    }
}