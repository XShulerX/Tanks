using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{

    public sealed class Enemy : MonoBehaviour, IEnemy, ITakeDamageEnemy
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
        private int _currentHealthPoints;
        private BulletPool _bulletPool;

        [SerializeField]
        private int _currentHealthPointsDefault;
        [SerializeField]
        private Slider _sliderHP;

        private int _maxEnemyHPOnLevel = 5;

        public Slider GetSlider { get => _sliderHP; }
        public int MaxHealthEnemyPoints { get=> _maxEnemyHPOnLevel; set=> _maxEnemyHPOnLevel = value; }
        public int GetDefaultEnemyHP { get => _currentHealthPointsDefault; }

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public event Action<Vector3> OnMouseUpChange;

        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        public Elements TankElement { get; set; }
        public int CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if (value <= 0)
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

        public Transform Turret { get => _turret; set => _turret = value; }
        public GameObject GetWrackObject { get => _wrackObject; }
        public ParticleSystem GetParticleExplosion { get => _tankObjectExplosion; }
        public GameObject GetTankObject { get => _tankObject; }
        public Material Material { get; set; }

        private void Start()
        {
            IsDead = false;
            IsYourTurn = false;

            var elements = Enum.GetValues(typeof(Elements));
            TankElement = (Elements)UnityEngine.Random.Range(1, elements.Length);
            Material = TankElement switch
            {
                Elements.Fire => Resources.Load("ElementMaterials/Fire") as Material,
                Elements.Terra => Resources.Load("ElementMaterials/Terra") as Material,
                Elements.Water => Resources.Load("ElementMaterials/Water") as Material,
            };

            var materials = _turret.GetComponent<MeshRenderer>().materials;
            materials[0] = Material;
            _turret.GetComponent<MeshRenderer>().materials = materials;
        }


        public Enemy SetPool(BulletPool pool)
        {
            _bulletPool = pool;
            return this;
        }

        public void Fire(Transform target)
        {
            _turret.LookAt(new Vector3(target.position.x, _turret.position.y, target.position.z));
            var bullet = _bulletPool.GetFreeElement();
            bullet.transform.position = _gun.position;
            bullet.transform.rotation = _gun.rotation;
            bullet.GetComponent<MeshRenderer>().material = Material;
            var bulletEntety = bullet.GetComponent<Bullet>();
            bulletEntety.SetContainer(_bulletPool.GetContainer);
            bulletEntety.InvokeTimer();
            bulletEntety.element = TankElement;
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * 100, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsDead) return;
            OnCollisionEnterChange?.Invoke(collision, this);
            _sliderHP.value = (float)_currentHealthPoints / (float)_maxEnemyHPOnLevel;
        }

        private void OnMouseUp()
        {
            if (IsDead) return;
            OnMouseUpChange?.Invoke(transform.position);
        }
    }
}