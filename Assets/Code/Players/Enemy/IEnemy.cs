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
        public Enemy SetPool(BulletPool pool);
        public void SetDamageModifer(float modifer);
        public Material Material { get; set; }
        public void Reset(float _forceModifer, float startHP);
    }
}