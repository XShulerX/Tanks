using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVC
{

    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private Transform _turret;

        private int _currentHealthPoints;

        public event Action OnCollisionEnterChange;
        public bool isYourTurn { get ; set; }

        private void Start()
        {
            isYourTurn = false;
            _currentHealthPoints = 2;
        }

        public void Fire(Transform target)
        {
            _turret.LookAt(new Vector3(target.position.x, _turret.position.y, target.position.z));
            var bullet = Object.Instantiate(_bullet, _gun.position, _gun.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * 100, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke();
        }
    }
}