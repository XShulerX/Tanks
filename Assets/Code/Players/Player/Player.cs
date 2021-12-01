using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public Action<IGamer> wasKilled { get; set; } = delegate (IGamer s) { };

        private AliveStateController _aliveStateController;
        private GroundStateController _groundStateController;
        private Controllers _controllers;
        private Dictionary<int, Ability> _abilities = new Dictionary<int, Ability>();

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
        [SerializeField]
        private GameObject _circleOfChoice;

        private IEnemy _target;
        private Material _fire;
        private Material _terra;
        private Material _water;

        /// <summary>
        public Transform Turret { get => _turret; set => _turret = value; }
        public Transform GetGun { get => _gun; }
        public GameObject GetWrackObject { get => _wrackObject; }
        public ParticleSystem GetParticleExplosion { get => _tankObjectExplosion; }
        public GameObject GetTankObject { get => _tankObject; }
        /// </summary>

        public event Action<Collision, ITakeDamage> OnCollisionEnterChange;
        public bool IsYourTurn { get ; set; }
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
                    if (!_aliveStateController.State.IsDead)
                    {
                        wasKilled.Invoke(this);
                    }
                }
                _currentHealthPoints = value;
            }
        }

        public AliveStateController AliveStateController { get => _aliveStateController; }
        public GroundStateController GroundStateController { get => _groundStateController; }
        public Dictionary<int, Ability> Abilities { get => _abilities;}
        public GameObject CircleOfChoice { get => _circleOfChoice; }

        public void Init(Controllers controllers)
        {
            _controllers = controllers;
            _aliveStateController = new AliveStateController(this);
            _groundStateController = new GroundStateController(this, _controllers);
        }

        private void Awake()
        {
            _fire = Resources.Load("ElementMaterials/Fire") as Material;
            _terra = Resources.Load("ElementMaterials/Terra") as Material;
            _water = Resources.Load("ElementMaterials/Water") as Material;
            
            _currentHealthPoints = maxHP;
            _sliderHP.value = _currentHealthPoints / maxHP;

            var elements = Enum.GetValues(typeof(Elements));
            TankElement = (Elements)UnityEngine.Random.Range(1, elements.Length);
            SetTurretAndIconColor();
        }

        private void SetTurretAndIconColor()
        {
            Material = TankElement switch
            {
                Elements.Fire => _fire,
                Elements.Terra => _terra,
                Elements.Water => _water,
            };

            var materials = _turret.GetComponent<MeshRenderer>().materials;
            materials[0] = Material;
            _turret.GetComponent<MeshRenderer>().materials = materials;

            GamerIconElement = GetComponentInChildren<Image>();
            GamerIconElement.color = MaterialAssociationMap.GetColorForMaterial(Material);
        }

        public void UpdateHelthView()
        {
            _sliderHP.value = _currentHealthPoints / maxHP;
        }

        public void Reset()
        {
            CurrentHealthPoints = maxHP;
            _sliderHP.value = _currentHealthPoints / maxHP;
            GetWrackObject.SetActive(false);
            GetTankObject.SetActive(true);
            _circleOfChoice.SetActive(false);
            _aliveStateController.SetAliveState();
            IsShoted = false;
            IsYourTurn = true;
            _turret.rotation = _turret.parent.rotation;
            UpdateHelthView();
        }

        public void SwapTarget(IEnemy enemy)
        {
            if (TryGetTarget(out IEnemy target))
            {
                target.ShowOrHideCircle(false);
            }
            _target = enemy;
            _target.ShowOrHideCircle(true);
            var targetPosition = _target.transform.position;
            _turret.LookAt(new Vector3(targetPosition.x, _turret.position.y, targetPosition.z));
        }

        public bool TryGetTarget(out IEnemy target)
        {
            target = _target;
            if (target is null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void SetTargetAsNull()
        {
            _target.ShowOrHideCircle(false);
            _target = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke(collision, this);
            _sliderHP.value = _currentHealthPoints / maxHP;
        }

        void ILoadeble.Load(IMementoData mementoData)
        {
            if(mementoData is PlayerMementoData playerMemento)
            {
                CurrentHealthPoints = playerMemento.hp;
                TankElement = playerMemento.element;
                UpdateHelthView();
                SetTurretAndIconColor();
            }
            else
            {
                throw new Exception("Передан не тот mementoData");
            }
        }
    }
}