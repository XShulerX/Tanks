using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class FireTargetAbilityController : IExecute
    {
        private List<BulletPool> _bulletPools;
        private TimerController _timerController;
        private PoolModel _poolModel;
        private List<Bullet> _bullets;
        private Player _player;

        private const int NUMBER_OF_BULLETS = 5;
        private const float TIME_OF_ACTIVATION_BULLET = 1f;

        public FireTargetAbilityController(List<BulletPool> bulletPools, TimerController timerController, PoolModel poolModel)
        {
            _poolModel = poolModel;
            _bullets = new List<Bullet>();
            _player = GameObject.FindObjectOfType<Player>();
            _bulletPools = bulletPools;
            _timerController = timerController;
        }


        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                for(int i = 0; i< _bulletPools.Count; i++)
                {
                    if(_bulletPools[i].GetPrefab.layer == 4)
                    {
                        for(int j=0; j< NUMBER_OF_BULLETS; j++)
                        {
                            var timer = new TimeData(TIME_OF_ACTIVATION_BULLET + j * 2);
                            timer.OnTimerEndWithBool += Fire;
                            _timerController.AddTimer(timer);
                        }
                        var timer_1 = new TimeData(TIME_OF_ACTIVATION_BULLET + NUMBER_OF_BULLETS * 2);
                        timer_1.OnTimerEndWithBool += EnemyTurn;
                        _timerController.AddTimer(timer_1);
                    }
                }
            }

            for(int i=0; i< _bullets.Count; i++)
            {
                if (_bullets[i].GetCollisionObject != null && _bullets[i].gameObject.layer == 4 && _bullets[i].GetCollisionObject.layer == 11)
                {
                    Debug.Log("true");
                    _bullets[i].gameObject.transform.position = _poolModel.GetContainer.position;
                    _bullets[i].GetCollisionObject = null;
                    _bullets[i].gameObject.SetActive(false);
                    _bullets.RemoveAt(i);
                }
            }
            
        }

        public void Fire()
        {
            Debug.Log("Fire");
           if (_bulletPools[0].HasFreeElement(out var element))
           {
                _bullets.Add(element.GetComponent<Bullet>());
                element.transform.position = _player.GetGun.position;
                element.transform.rotation = _player.GetGun.rotation;
                element.GetComponent<Rigidbody>().AddForce(_player.GetGun.forward * 40, ForceMode.Impulse);
                element.GetComponent<Bullet>().SetContainer(_bulletPools[0].GetContainer);
                element.GetComponent<Bullet>().InvokeTimer();
            }


        }

        public void EnemyTurn()
        {
            Debug.Log("Turn");
            _player.IsYourTurn = false;
        }
    }
}

