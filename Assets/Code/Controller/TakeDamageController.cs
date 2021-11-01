using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    internal sealed class TakeDamageController : IInitialization, ICleanup
    {
        private readonly IEnumerable<ITakeDamage> _players;

        public TakeDamageController(IEnumerable<ITakeDamage> players)
        {
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
            var damage = bullet.gameObject.GetComponent<Bullet>().Damage;
            player.CurrentHealthPoints -= damage; 
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