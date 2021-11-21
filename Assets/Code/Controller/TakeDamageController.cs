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
            foreach (var player in _players)
            {
                player.OnCollisionEnterChange += TakeDamage;
            }
        }

        public void Initilazation()
        {

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
            else if(player is ITakeDamagePlayer)
            {
                var damageModifier = player as ITakeDamagePlayer;
                var damage = bullet.gameObject.GetComponent<Bullet>().Damage;
                player.CurrentHealthPoints -= damage * damageModifier.DamageMultiplier;
                Debug.Log(damage * damageModifier.DamageMultiplier);
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