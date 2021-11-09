using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class FireAbility : Ability
    {
        private GameObject _box;
        private List<Bullet> _bullets = new List<Bullet>();
        public FireAbility(int cooldown, TimerController timerController, BulletPool pool, GameObject box) : base(cooldown, timerController, pool)
        {
            _cooldown = cooldown;
            _timerController = timerController;
            _pool = pool;
            _box = box;
        }

        public override void ActivateAbility()
        {
            _box.transform.Translate(Vector3.up * 10);
            var timer = new TimeData(1f);
            timer.OnTimerEndWithBool += BoxBlowUp;
            _timerController.AddTimer(timer);
        }

        private void BoxBlowUp()
        {
            var spawnPosition = new Vector3(_box.transform.position.x, _box.transform.position.y + 2f, _box.transform.position.z);
            _box.transform.Translate(Vector3.down * 10);

            for (int i = 0; i < 50; i++)
            {
                var bullet = _pool.GetFreeElement();
                bullet.transform.position = spawnPosition;
                bullet.transform.rotation = Random.rotation;
                var bulletEntity = bullet.GetComponent<Bullet>();
                bulletEntity.element = Elements.Water;
                _bullets.Add(bulletEntity);
            }
            var timer = new TimeData(1f);
            timer.OnTimerEndWithBool += EndWaterAbility;
            _timerController.AddTimer(timer);
        }

        private void EndWaterAbility()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].SetContainer(_pool.GetContainer);
                _bullets[i].InvokeTimer();
            }
            abilityIsEnded.Invoke();
            _bullets.Clear();
            _isOnCooldown = true;
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