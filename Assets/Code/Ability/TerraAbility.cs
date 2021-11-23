using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TerraAbility : Ability
    {
        public Player player;
        public UnitStorage unitStorage;

        public TerraAbility(BulletPool pool, AbilityModel abilityModel) : base(pool, abilityModel)
        {
        }

        public override void ActivateAbility()
        {
            var element = _pool.GetFreeElement();
            player.SwapTarget(GetRandomEnemy());
            element.transform.position = player.GetGun.position;
            element.transform.rotation = player.GetGun.rotation;
            element.GetComponent<MeshRenderer>().material = _material;
            element.GetComponent<Rigidbody>().AddForce(player.GetGun.forward * 40, ForceMode.Impulse);
            var bulletEntity = element.GetComponent<Bullet>();
            bulletEntity.element = Elements.Terra;
            bulletEntity.SetContainer(_pool.GetContainer);
            bulletEntity.InvokeTimer();

            abilityIsEnded.Invoke();
            _isOnCooldown = true;
        }

        private Vector3 GetRandomEnemy()
        {
            List<IEnemy> liveEnemies = new List<IEnemy>();
            Vector3 enemy = Vector3.forward;

            for (int i = 0; i < unitStorage.Enemies.Count; i++)
            {
                if (!unitStorage.Enemies[i].IsDead)
                {
                    liveEnemies.Add(unitStorage.Enemies[i]);
                }
            }

            if(liveEnemies.Count > 0)
            {
                var playerTarget = Random.Range(0, liveEnemies.Count - 1);
                enemy = liveEnemies[playerTarget].transform.position;
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