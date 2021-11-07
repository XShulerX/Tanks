using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class FireTargetAbilityController : IExecute
    {
        private List<BulletPool> _bulletPools;
        private TimerController _timerController;
        private PoolModel _poolModel;
        private List<Bullet> _bullets = new List<Bullet>();
        private Player _player;
        private GameObject _box;
        private GameObject _abilityPanel;
        private int _abilityRandom;
        private TurnController _turnController;
        private bool _isAbilityUsed;

        private const int NUMBER_OF_BULLETS = 2;
        private const float TIME_OF_ACTIVATION_BULLET = 1f;
        private const int WATER = 0;
        private const int TERRA = 2;

        public FireTargetAbilityController(List<BulletPool> bulletPools, TimerController timerController, PoolModel poolModel, GameObject box, TurnController turnController, GameObject abilityPanel)
        {
            _turnController = turnController;
            _turnController.endGlobalTurn += RandomizeAbility;
            _box = box;
            _abilityPanel = abilityPanel;
            _poolModel = poolModel;
            _player = GameObject.FindObjectOfType<Player>();
            _bulletPools = bulletPools;
            _timerController = timerController;
            RandomizeAbility();
        }


        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_isAbilityUsed & _player.IsYourTurn)
                {
                    _player.IsShoted = true;
                    if (_abilityRandom <= 50)
                    {
                        WaterAbility();
                    }
                    else
                    {
                        TerraAbility();
                    }
                    _isAbilityUsed = true;
                }
            }

            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].GetCollisionObject != null && (_bullets[i].gameObject.layer == 4 || _bullets[i].gameObject.layer == 6) && _bullets[i].GetCollisionObject.layer == 11)
                {
                    Debug.Log("true");
                    _bullets[i].gameObject.transform.position = _poolModel.GetContainer.position;
                    _bullets[i].GetCollisionObject = null;
                    _bullets[i].gameObject.SetActive(false);
                    _bullets.RemoveAt(i);
                }
            }

        }

        private void WaterAbility() // ������, ����������� ��� ������ �� ���� ��� ������, ��� ��� �� ��������, ��� ������, �� � ����� ������������ �������)))
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
                var bullet = _bulletPools[WATER].GetFreeElement();
                bullet.transform.position = spawnPosition;
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
                _bullets[i].SetContainer(_bulletPools[WATER].GetContainer);
                _bullets[i].InvokeTimer();
            }
            EnemyTurn();
        }

        private void TerraAbility()
        {
            for (int j = 0; j < NUMBER_OF_BULLETS; j++)
            {
                var timer = new TimeData(TIME_OF_ACTIVATION_BULLET + j * 2);
                timer.OnTimerEndWithBool += TerraShot;
                _timerController.AddTimer(timer);
            }
            var timer_1 = new TimeData(TIME_OF_ACTIVATION_BULLET + NUMBER_OF_BULLETS * 2);
            timer_1.OnTimerEndWithBool += EnemyTurn;
            _timerController.AddTimer(timer_1);
        }

        public void TerraShot()
        {
            Debug.Log("Fire");
            if (_bulletPools[TERRA].HasFreeElement(out var element))
            {
                _bullets.Add(element.GetComponent<Bullet>());
                element.transform.position = _player.GetGun.position;
                element.transform.rotation = _player.GetGun.rotation;
                element.GetComponent<Rigidbody>().AddForce(_player.GetGun.forward * 40, ForceMode.Impulse);
                var bulletEntity = element.GetComponent<Bullet>();
                bulletEntity.element = Elements.Terra;
                bulletEntity.SetContainer(_bulletPools[0].GetContainer);
                bulletEntity.InvokeTimer();
            }
        }

        public void EnemyTurn()
        {
            Debug.Log("Turn");
            _player.IsYourTurn = false;
            _isAbilityUsed = false;
            _player.IsShoted = false;
        }

        private void RandomizeAbility()
        {
            _abilityRandom = Random.Range(0, 100);
            if (_abilityRandom <= 50)
            {
                _abilityPanel.GetComponent<Image>().color = Color.blue;
            }
            else
            {
                _abilityPanel.GetComponent<Image>().color = Color.black;
            }
        }
    }
}

