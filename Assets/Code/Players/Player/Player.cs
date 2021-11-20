using System;
using UnityEngine;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public Action<IGamer> wasKilled { get; set; } = delegate (IGamer s) { };

        [SerializeField]
        private ParticleSystem _tankObjectExplosion;
        [SerializeField]
        private GameObject _tankObject;
        [SerializeField]
        private GameObject _wrackObject;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private Transform _turret;

        private Vector3 _target;
        private float _currentHealthPoints;



        /// <summary>
        public Transform GetGun { get => _gun; }
        public GameObject GetWrackObject { get => _wrackObject; }
        public ParticleSystem GetParticleExplosion { get => _tankObjectExplosion; }
        public GameObject GetTankObject { get => _tankObject; }
        /// </summary>

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        public float CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if(value <= 0)
                {
                    if (!IsDead)
                    {
                        wasKilled.Invoke(this);
                    }
                    IsDead = true;
                }
                _currentHealthPoints = value;
            }
        }

        internal void Reset(float maxHP)
        {
            CurrentHealthPoints = maxHP;
            GetWrackObject.SetActive(false);
            GetTankObject.SetActive(true);
            IsDead = false;
            IsShoted = false;
            IsYourTurn = true;
            _turret.rotation = _turret.parent.rotation;
        }

        public Player()
        {
            IsYourTurn = true;
            IsDead = false;
            _currentHealthPoints = 20;
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