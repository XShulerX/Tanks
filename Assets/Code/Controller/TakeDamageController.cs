using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class TakeDamageController : IInitialization, ICleanup
    {
        private readonly IEnumerable<ITakeDamage> _players;
        private ElementsController _elementsController;

        public TakeDamageController(IEnumerable<ITakeDamage> players, ElementsController elementsController)
        {
            _elementsController = elementsController;
            _players = players;
        }

        public void Initilazation()
        {
            foreach(var player in _players)
            {
                player.OnCollisionEnterChange += TakeDamage;
            }
        }

        private void TakeDamage(Collision bullet, ITakeDamage player)
        {
            if (player is ITakeDamageEnemy)
            {
                var bulletEntity = bullet.gameObject.GetComponent<Bullet>();
                var enemy = player as ITakeDamageEnemy;
                var elementModifer = _elementsController.GetModifer(enemy, bulletEntity.element);
                enemy.CurrentHealthPoints -= bulletEntity.Damage * elementModifer;
            }
            else
            {
                var damage = bullet.gameObject.GetComponent<Bullet>().Damage;
                player.CurrentHealthPoints -= damage;
            }
        }

        public void Cleanup()
        {
            foreach (var player in _players)
            {
                player.OnCollisionEnterChange -= TakeDamage;
            }
        }
    }
}