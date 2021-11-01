using System;
using UnityEngine;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private Transform _turret;

        private Vector3 _target;
        private int _currentHealthPoints;

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public int CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if(value < 0)
                {
                    IsDead = true;
                }
                _currentHealthPoints = value;
            }
        }

        public Player()
        {
            IsYourTurn = true;
            IsDead = false;
            _currentHealthPoints = 100;
        }

        public void Fire()
        {
            var bullet = Instantiate(_bullet, _gun.position, _gun.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * 100, ForceMode.Impulse);
        }

        public void SwapTarget(Vector3 newTarget)
        {
            _target = newTarget;
            _turret.LookAt(new Vector3(_target.x, _turret.position.y, _target.z));
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke(collision, this);
        }
    }
}