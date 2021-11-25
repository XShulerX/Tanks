using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class TakeDamageController : IInitialization, ICleanup
    {
        private UnitStorage _unitStorage;
        private ElementsController _elementsController;

        public TakeDamageController(UnitStorage unitStorage, ElementsController elementsController)
        {
            _elementsController = elementsController;
            _unitStorage = unitStorage;
        }

        public void Initilazation()
        {
            foreach(var player in _unitStorage.Gamers)
            {
                player.OnCollisionEnterChange += TakeDamage;
            }
        }

        private void TakeDamage(Collision bullet, ITakeDamage player)
        {
            var bulletEntity = bullet.gameObject.GetComponent<Bullet>();
            var elementModifer = _elementsController.GetModifer(player, bulletEntity.element);
            player.CurrentHealthPoints -= bulletEntity.Damage * elementModifer;
        }

        public void Cleanup()
        {
            foreach (var player in _unitStorage.Gamers)
            {
                player.OnCollisionEnterChange -= TakeDamage;
            }
        }
    }
}