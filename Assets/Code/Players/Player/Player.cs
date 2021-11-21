using System;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField]
        private Slider _sliderHP;

        private Vector3 _target;
        private int _currentHealthPoints;

        private int _damageMultiplierDefault = 1;
        private int _damageMultiplier = 1;


        /// <summary>
        public Transform GetGun { get => _gun; }
        public GameObject GetWrackObject { get => _wrackObject; }
        public ParticleSystem GetParticleExplosion { get => _tankObjectExplosion; }
        public GameObject GetTankObject { get => _tankObject; }
        public int DemageMultiplierDefault { get => _damageMultiplierDefault; }
        public int DamageMultiplier { get => _damageMultiplier; set => _damageMultiplier = value; }
        public Slider GetSlider { get => _sliderHP; }
        public int MaxHealthPlayerPoints { get => 100; }
        /// </summary>

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        public int CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if(value <= 0)
                {
                    if (!IsDead)
                    {
                        wasKilled.Invoke(this);
                        GameOver();
                    }
                    IsDead = true;
                }
                _currentHealthPoints = value;
            }
        }

        private void GameOver()
        {
            Debug.Log("Lose");
            //Time.timeScale = 0;

        }

        public Player()
        {
            IsYourTurn = true;
            IsDead = false;
            _currentHealthPoints = 100;
        }

        public void SwapTarget(Vector3 newTarget)
        {
            _target = newTarget;
            _turret.LookAt(new Vector3(_target.x, _turret.position.y, _target.z));
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke(collision, this);
            _sliderHP.value = (float)_currentHealthPoints / (float)MaxHealthPlayerPoints;
        }
    }
}