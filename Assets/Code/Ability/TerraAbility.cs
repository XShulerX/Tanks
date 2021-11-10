using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TerraAbility : Ability
    {
        private Player _player;
        private List<IEnemy> _enemies = new List<IEnemy>();

        public TerraAbility(int cooldown, BulletPool pool, Player player, List<IEnemy> enemies) : base(cooldown, pool)
        {
            _enemies = enemies;
            _player = player;
            _cooldown = cooldown;
            _pool = pool;
        }

        public override void ActivateAbility()
        {
            if (_pool.HasFreeElement(out var element))
            {
                _player.SwapTarget(GetRandomEnemy());
                element.transform.position = _player.GetGun.position;
                element.transform.rotation = _player.GetGun.rotation;
                element.GetComponent<Rigidbody>().AddForce(_player.GetGun.forward * 40, ForceMode.Impulse);
                var bulletEntity = element.GetComponent<Bullet>();
                bulletEntity.element = Elements.Terra;
                bulletEntity.SetContainer(_pool.GetContainer);
                bulletEntity.InvokeTimer();
            }
            abilityIsEnded.Invoke();
            _isOnCooldown = true;
        }

        private Vector3 GetRandomEnemy()
        {
            List<IEnemy> liveEnemies = new List<IEnemy>();
            Vector3 enemy = Vector3.forward;

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].IsDead)
                {
                    liveEnemies.Add(_enemies[i]);
                }
            }

            if(liveEnemies.Count > 0)
            {
                var playerTarget = Random.Range(0, liveEnemies.Count - 1);
                enemy = _enemies[playerTarget].transform.position;
            }
  
            return enemy;
        }


        public override void ReduceCooldown()
        {
            _cooldownTurns++;
            if (_cooldownTurns == _cooldown)
            {
                _isOnCooldown = false;
                _cooldownTurns = 0;
            }
        }
    }
}