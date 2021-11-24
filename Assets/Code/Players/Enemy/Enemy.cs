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
        private float maxHP;
        [SerializeField]
        private float _currentHealthPoints;
        [SerializeField]
        private Slider _sliderEnemyHP;

        private BulletPool _bulletPool;
        private float _forceModifer = 1;
        private int _id;

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public event Action<Vector3> OnMouseUpChange;

        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        public Elements TankElement { get; set; }
        public float CurrentHealthPoints {
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
        public Image GamerIconElement { get; private set; }
        public int Id { get => _id; }
        public float ForceModifer { get => _forceModifer; }

        private void Awake()
        {
            IsDead = false;
            IsYourTurn = false;
            _currentHealthPoints = maxHP;
            _sliderEnemyHP.value = _currentHealthPoints / maxHP;

            var elements = Enum.GetValues(typeof(Elements));
            TankElement = (Elements)UnityEngine.Random.Range(1, elements.Length);
            Material = TankElement switch
            {
                Elements.Fire => Resources.Load("ElementMaterials/Fire") as Material,
                Elements.Terra => Resources.Load("ElementMaterials/Terra") as Material,
                Elements.Water => Resources.Load("ElementMaterials/Water") as Material,
            };
        }

        private void Start()
        {
            GamerIconElement = GetComponentInChildren<Image>();

            var materials = _turret.GetComponent<MeshRenderer>().materials;
            materials[0] = Material;
            _turret.GetComponent<MeshRenderer>().materials = materials;
            GamerIconElement.color = MaterialAssociationMap.GetColorForMaterial(Material);
        }

        public Enemy SetPool(BulletPool pool)
        {
            _bulletPool = pool;
            return this;
        }

        public Enemy SetID(int id)
        {
            _id = id;
            return this;
        }

        public void Fire(Transform target)
        {
            if (target.GetComponent<Player>().IsDead) return;
            
            _turret.LookAt(new Vector3(target.position.x, _turret.position.y, target.position.z));
            var bullet = _bulletPool.GetFreeElement();
            bullet.transform.position = _gun.position;
            bullet.transform.rotation = _gun.rotation;
            bullet.GetComponent<MeshRenderer>().material = Material;
            var bulletEntety = bullet.GetComponent<Bullet>();
            bulletEntety.Damage *= _forceModifer;
            bulletEntety.SetContainer(_bulletPool.GetContainer);
            bulletEntety.InvokeTimer();
            bulletEntety.element = TankElement;
            bullet.GetComponent<Rigidbody>().AddForce(_gun.forward * 100, ForceMode.Impulse);
        }

        public void SetDamageModifer(float modifer)
        {
            _forceModifer = modifer;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsDead) return;
            OnCollisionEnterChange?.Invoke(collision, this);
            _sliderEnemyHP.value = _currentHealthPoints / maxHP;
        }

        private void OnMouseUp()
        {
            if (IsDead) return;
            OnMouseUpChange?.Invoke(transform.position);
        }

        public void Reset(float forceModifer)
        {
            maxHP *= forceModifer;
            CurrentHealthPoints = maxHP;
            _sliderEnemyHP.value = _currentHealthPoints / maxHP;
            SetDamageModifer(forceModifer);
            GetWrackObject.SetActive(false);
            GetTankObject.SetActive(true);
            IsDead = false;
            IsShoted = false;
            IsYourTurn = false;
            _turret.rotation = _turret.parent.rotation;
        }
    }
}