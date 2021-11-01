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

        public bool isYourTurn { get ; set; }

        public event Action OnCollisionEnterChange;

        private void Start()
        {
            isYourTurn = false;
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