using System;
using UnityEngine;

namespace MVC
{

    public sealed class Enemy : MonoBehaviour, IEnemy, ITakeDamageEnemy
    {
        public Action<IGamer> wasKilled { get; set; } = delegate (IGamer s) { };

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
        public bool IsShoted { get; set; }
        public Elements TankElement { get; set; }
        public int CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if (value <= 0)
                {
                    if (!IsDead) wasKilled.Invoke(this);
                    IsDead = true;
                    
                }
                _currentHealthPoints = value;
            }
        }

        public Transform Turret { get => _turret; set => _turret = value; }

        private void Start()
        {
            IsDead = false;
            IsYourTurn = false;

            var elements = Enum.GetValues(typeof(Elements));
            TankElement = (Elements)UnityEngine.Random.Range(1, elements.Length);
            var turretMaterial = TankElement switch
            {
                Elements.Fire => Resources.Load("ElementMaterials/Fire") as Material,
                Elements.Terra => Resources.Load("ElementMaterials/Terra") as Material,
                Elements.Water => Resources.Load("ElementMaterials/Water") as Material,
            };

            var materials = _turret.GetComponent<MeshRenderer>().materials;
            materials[0] = turretMaterial;
            _turret.GetComponent<MeshRenderer>().materials = materials;
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