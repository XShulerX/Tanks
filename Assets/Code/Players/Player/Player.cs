using System;
using System.Collections.Generic;
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
        private float maxHP;
        [SerializeField]
        private float _currentHealthPoints;
        [SerializeField]
        private Slider _sliderHP;

        private Vector3 _target;

        /// <summary>
        public Transform Turret { get => _turret; set => _turret = value; }
        public Transform GetGun { get => _gun; }
        public GameObject GetWrackObject { get => _wrackObject; }
        public ParticleSystem GetParticleExplosion { get => _tankObjectExplosion; }
        public GameObject GetTankObject { get => _tankObject; }
        /// </summary>

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public bool IsYourTurn { get ; set; }
        public bool IsDead { get; set; }
        public bool IsShoted { get; set; }
        public Elements TankElement { get; set; }
        public Material Material { get; set; }
        public Image GamerIconElement { get; private set; }

        public float CurrentHealthPoints {
            get => _currentHealthPoints;
            set
            {
                if(value <= 0)
                {
                    if (!IsDead)
                    {
                        wasKilled.Invoke(this);

                        ColorBlock colors = _sliderHP.colors;
                        colors.disabledColor = Color.red;
                        _sliderHP.colors = colors;
                    }
                    IsDead = true;
                }
                _currentHealthPoints = value;
            }
        }

        public Player()
        {
            IsYourTurn = true;
            IsDead = false;
        }

        private void Awake()
        {
            _currentHealthPoints = maxHP;
            _sliderHP.value = _currentHealthPoints / maxHP;

            var elements = Enum.GetValues(typeof(Elements));
            TankElement = (Elements)UnityEngine.Random.Range(1, elements.Length);
            GamerIconElement = GetComponentInChildren<Image>();
            UpdateTurretMaterialFromLoad();
        }

        public void UpdateTurretMaterialFromLoad()
        {
            Material = TankElement switch
            {
                Elements.Fire => Resources.Load("ElementMaterials/Fire") as Material,
                Elements.Terra => Resources.Load("ElementMaterials/Terra") as Material,
                Elements.Water => Resources.Load("ElementMaterials/Water") as Material,
            };
            var materials = _turret.GetComponent<MeshRenderer>().materials;
            materials[0] = Material;
            _turret.GetComponent<MeshRenderer>().materials = materials;
            GamerIconElement.color = MaterialAssociationMap.GetColorForMaterial(Material);
        }

        public void Reset()
        {
            CurrentHealthPoints = maxHP;
            _sliderHP.value = _currentHealthPoints / maxHP;
            GetWrackObject.SetActive(false);
            GetTankObject.SetActive(true);
            IsDead = false;
            IsShoted = false;
            IsYourTurn = true;
            _turret.rotation = _turret.parent.rotation;
        }

        public void SwapTarget(Vector3 newTarget)
        {
            _target = newTarget;
            _turret.LookAt(new Vector3(_target.x, _turret.position.y, _target.z));
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke(collision, this);
            _sliderHP.value = _currentHealthPoints / maxHP;
        }
        public void UpdateHelthView()
        {
            _sliderHP.value = _currentHealthPoints / maxHP;

            ColorBlock colors = _sliderHP.colors;
            if (_currentHealthPoints > 0)
            {
                colors.disabledColor = Color.green;
            } else
            {
                colors.disabledColor = Color.red;
            }
            
            _sliderHP.colors = colors;
        }
    }
}