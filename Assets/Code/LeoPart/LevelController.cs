using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class LevelController : IExecute
    {
        private TurnController _turnController;
        private Controllers _controllers;
        private TimerController _timerController;
        private ElementsController _elementsController;
        private Text _text;
        private List<IGamer> _gamers;
        private EnemyFactory _enemyFactory;
        private Player _player;
        private List<IEnemy> _enemies;
        private PlayerTargetController _playerTargetController;
        private GameObject _box;
        private BulletPoolsInitialization _bulletPoolsInitialization;
        private UIInitializationModel _uiModel;
        private AbilitiesData _abilitiesData;
        private PlayerAbilityController _playerAbilityController;
        private UIAbilityPanelsStateController _uiStateController;
        private EnemyFireController _enemyFireController;
        private TakeDamageController _takeDamageController;

        private int _damageModifier;
        private int _enemyMaxHP;
        private int _previousEnemyHP;
        private bool _isNextLevel = false;
        private bool _isNewTry = false;

        private int _numberOfTry = 0;

        private const int NUMBER_OF_TRY = 3; 

        public LevelController(Controllers controllers, TimerController timerController, Text text, EnemyFactory enemyFactory, Player player, GameObject box, BulletPoolsInitialization bulletPoolsInitialization, UIInitializationModel uimodel, AbilitiesData abilitiesData)
        {
            _abilitiesData = abilitiesData;
            _uiModel = uimodel;
            _bulletPoolsInitialization = bulletPoolsInitialization;
            _box = box;
            _controllers = controllers;
            _timerController = timerController;
            _text = text;
            _uiModel = uimodel;

            _enemyFactory = enemyFactory;
            _player = player;
            _gamers = new List<IGamer>();

            _enemies = new List<IEnemy>();

            _enemies.AddRange(_enemyFactory.CreateEnemies());
            _gamers.Add(_player);
            _gamers.AddRange(_enemies);

            NewInitializeLevel();

            _damageModifier = _player.DamageMultiplier;
            _enemyMaxHP = _enemies[0].CurrentHealthPoints;
            _previousEnemyHP = _enemies[0].CurrentHealthPoints;
        }


        public void Execute(float deltaTime)
        {
            if (_turnController.IsAllEnemiesDead)
            {

                Destroyenemies();
                _isNextLevel = true;

                var timer = new TimeData(2f, _timerController);
                timer.TimerEnd += SetNewLevel;
                _turnController.IsAllEnemiesDead = false;
            }
            else if (_player.IsDead == true)
            {
                if(_numberOfTry < NUMBER_OF_TRY)
                {
                    _numberOfTry++;
                    _isNewTry = true;
                }
                else if(_numberOfTry == NUMBER_OF_TRY)
                {
                    _numberOfTry = 0;
                    _isNewTry = false;
                }

                RemoveControllers();
                Destroyenemies();

                _player.IsDead = false;

                var timer = new TimeData(2f, _timerController);
                timer.TimerEnd += SetNewLevel;
            }
        }

        private void Destroyenemies()
        {
            for (int i = 1; i < _gamers.Count; i++)
            {
                Object.DestroyImmediate((_gamers[i] as Enemy).gameObject);
            }
        }

        private void RemoveControllers()
        {
            _controllers._executeControllers.Remove(_turnController);
            _controllers._initializeControllers.Remove(_playerTargetController);
            _controllers._executeControllers.Remove(_playerAbilityController);
            _controllers._executeControllers.Remove(_uiStateController);
            _controllers._executeControllers.Remove(_enemyFireController);
            _controllers._initializeControllers.Remove(_takeDamageController);
        }
        
        private void ClearLists()
        {
            _enemies.Clear();
            _enemies.AddRange(_enemyFactory.CreateEnemies());
            _gamers.Clear();
            _gamers.Add(_player);
            _gamers.AddRange(_enemies);
        }

        public void SetNewLevel()
        {
            RemoveControllers();
            ClearLists();


            if (_isNewTry)
            {
                _player.DamageMultiplier = _damageModifier;

                _player.CurrentHealthPoints = _player.MaxHealthPlayerPoints;
                _player.GetSlider.value = (float)_player.CurrentHealthPoints / (float)_player.MaxHealthPlayerPoints;
                for (int i = 1; i < _gamers.Count; i++)
                {
                    (_gamers[i] as Enemy).CurrentHealthPoints = _enemyMaxHP;
                    (_gamers[i] as Enemy).MaxHealthEnemyPoints = (_gamers[i] as Enemy).CurrentHealthPoints;
                }

                _isNewTry = false;
            }
            else if (_isNextLevel)
            {
                _previousEnemyHP += 5;
                for (int i =1; i< _gamers.Count; i++)
                {
                    
                    (_gamers[i] as Enemy).CurrentHealthPoints = _previousEnemyHP;
                    (_gamers[i] as Enemy).MaxHealthEnemyPoints = (_gamers[i] as Enemy).CurrentHealthPoints;
                }
                _enemyMaxHP = (_gamers[1] as Enemy).CurrentHealthPoints;
                _player.CurrentHealthPoints = _player.MaxHealthPlayerPoints;
                _player.GetSlider.value = (float)_player.CurrentHealthPoints / (float)_player.MaxHealthPlayerPoints;
                _player.DamageMultiplier++;
                _damageModifier = _player.DamageMultiplier;
                _isNextLevel = false;
            }
            else
            {
                _previousEnemyHP = 5;
                _player.DamageMultiplier = _player.DemageMultiplierDefault;
                _player.CurrentHealthPoints = _player.MaxHealthPlayerPoints;
                _player.GetSlider.value = (float)_player.CurrentHealthPoints / (float)_player.MaxHealthPlayerPoints;
            }
            _player.IsYourTurn = true;

            NewInitializeLevel();




        }
        private void NewInitializeLevel()
        {
            _elementsController = new ElementsController(_enemies);
            _turnController = new TurnController(_gamers, _timerController, _elementsController, _text);
            _controllers.Add(_turnController);
            new TankDestroyingController(_gamers, _timerController);

            _player.GetWrackObject.SetActive(false);
            _player.GetTankObject.SetActive(true);
            _playerTargetController = new PlayerTargetController(_enemies, _player);
            _controllers.Add(_playerTargetController);


            var abilityFactory = new AbilityFactory(_timerController, _player, _box, _enemies);

            _playerAbilityController = new PlayerAbilityController(_bulletPoolsInitialization.GetBullets, _turnController, _player, abilityFactory, _abilitiesData);
            _controllers.Add(_playerAbilityController);

            List<IRechargeableAbility> abilities = new List<IRechargeableAbility>();
            abilities.AddRange(_playerAbilityController.Abilities);
            _uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(_uiModel, abilities));
            _controllers.Add(_uiStateController);



            _enemyFireController = new EnemyFireController(_player.transform, _enemies);
            _takeDamageController = new TakeDamageController(_gamers, _elementsController);

            _controllers.Add(_enemyFireController);
            _controllers.Add(_takeDamageController);
        }

    }
}

