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
            foreach(var player in _unitStorage.gamers)
            {
                player.OnCollisionEnterChange += TakeDamage;
            }
        }

        private void TakeDamage(Collision bullet, ITakeDamage player)
        {
            var bulletEntity = bullet.gameObject.GetComponent<Bullet>();

            if (player is ITakeDamageEnemy)
            {
                var enemy = player as ITakeDamageEnemy;
                var elementModifer = _elementsController.GetModifer(enemy, bulletEntity.element);
                enemy.CurrentHealthPoints -= bulletEntity.Damage * elementModifer;
            }
            else
            {
                player.CurrentHealthPoints -= bulletEntity.Damage;
            }
        }

        public void Cleanup()
        {
            foreach (var player in _unitStorage.gamers)
            {
                player.OnCollisionEnterChange -= TakeDamage;
            }
        }
    }
}