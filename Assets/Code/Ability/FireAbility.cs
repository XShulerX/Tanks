using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class FireAbility : Ability
    {
        public GameObject box;
        public TimerController timerController;

        private List<Bullet> _bullets = new List<Bullet>();

        public FireAbility(BulletPool pool, AbilityModel abilityModel) : base(pool, abilityModel)
        {
        }

        public override void ActivateAbility()
        {
            box.transform.Translate(Vector3.up * 10);
            var timer = new TimerData(1f, timerController);
            timer.TimerIsOver += BoxBlowUp;
        }

        private void BoxBlowUp()
        {
            var spawnPosition = new Vector3(box.transform.position.x, box.transform.position.y + 2f, box.transform.position.z);
            box.transform.Translate(Vector3.down * 10);

            for (int i = 0; i < 50; i++)
            {
                var bullet = _pool.GetFreeElement();
                bullet.transform.position = spawnPosition;
                bullet.transform.rotation = Random.rotation;
                bullet.GetComponent<MeshRenderer>().material = _material;
                var bulletEntity = bullet.GetComponent<Bullet>();
                bulletEntity.element = Elements.Fire;
                _bullets.Add(bulletEntity);
            }
            var timer = new TimerData(1f, timerController);
            timer.TimerIsOver += EndFireAbility;
        }

        private void EndFireAbility()
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