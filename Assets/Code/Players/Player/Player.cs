using System;
using UnityEngine;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayerTurn
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private Transform _turret;

        private Vector3 _target;

        public event Action OnCollisionEnterChange;
        public bool isYourTurn { get ; set; }

        public Player()
        {
            isYourTurn = true;
        }

        public void SwapTarget(Vector3 newTarget)
        {
            _target = newTarget;
            _turret.LookAt(new Vector3(_target.x, _turret.position.y, _target.z));
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke();
        }
    }
}