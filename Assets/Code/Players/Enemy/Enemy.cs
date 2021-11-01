using System;
using UnityEngine;

namespace MVC
{

    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private Transform _turret;
        [SerializeField]
        private int _currentHealthPoints;

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public event Action<Vector3> OnMouseUpChange;

        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public int CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if (value < 0)
                {
                    IsDead = true;
                }
                _currentHealthPoints = value;
            }
        }

        private void Start()
        {
            IsDead = false;
            IsYourTurn = false;
            CurrentHealthPoints = 2;
        }

        public void Fire(Transform target)
        {
            _turret.LookAt(new Vector3(target.position.x, _turret.position.y, target.position.z));
            var bullet = Instantiate(_bullet, _gun.position, _gun.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * 100, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke(collision, this);
        }

        private void OnMouseUp()
        {
            OnMouseUpChange?.Invoke(transform.position);
        }
    }
}