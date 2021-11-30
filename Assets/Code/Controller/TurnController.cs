using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public sealed class TurnController : IExecute, IResetable, ILoadeble
    {
        public Action endGlobalTurn = delegate () { };
        public Action<Player> changeActivePlayer = delegate (Player player) { };

        private UnitStorage _unitStorage;

        private LinkedList<IGamer> _queueGamers;
        private bool _isTimerOver;
        private Player _activePlayer;
        private TimerController _timerController;
        private ElementsController _elementsController;
        private TimerData _timer;
        private Text _turnCountText;

        private int _shootedOrDeadEnemies;
        private int _globalTurnCount = 1;

        private const float DELAY_BEFOR_FIRE = 1f;

        public int GlobalTurnCount { get => _globalTurnCount; }
        public int ShootedOrDeadEnemies { get => _shootedOrDeadEnemies; }

        public TurnController(UnitStorage unitStorage, TimerController timerController, ElementsController elementsController, Text uiTurnCountText)
        {
            _unitStorage = unitStorage;

            _elementsController = elementsController;
            _queueGamers = new LinkedList<IGamer>(unitStorage.Gamers); // Перешел с очереди на Линкед лист для возможности перестановки
            _timerController = timerController;
            for (int i = 1; i < unitStorage.Gamers.Count; i++)
            {
                unitStorage.Gamers[i].wasKilled += AddDeadEnemy;
            }
            _turnCountText = uiTurnCountText;
            _turnCountText.text = "Ход 1";
        }

        public void Reset()
        {
            _globalTurnCount = 1;
            _turnCountText.text = "Ход 1";
            _shootedOrDeadEnemies = 0;
            _timer = null;

            for (int i = 0; i < _unitStorage.Gamers.Count; i++)
            {
                _unitStorage.Gamers[i].IsShoted = false;
                _unitStorage.Gamers[i].IsYourTurn = false;
            }
        }

        public void Execute(float deltaTime)
        {
            if (IsAllPlayersDie()) return;

            _activePlayer = _unitStorage.Players.Find(player => player.AliveStateController.State.IsAlive && !player.IsShoted);
            if (!(_activePlayer is null))
            {
                if (!_activePlayer.IsYourTurn)
                {
                    _activePlayer.IsYourTurn = true;
                    changeActivePlayer.Invoke(_activePlayer);
                    if (_activePlayer.TryGetTarget(out IEnemy target))
                    {
                        _activePlayer.SetTargetAsNull();
                    }
                }

            }
            else
            {
                var _activeEnemy = _unitStorage.Enemies.Find(enemy => !enemy.IsShoted && enemy.AliveStateController.State.IsAlive);
                if (!(_activeEnemy is null))
                {
                    if (_timer is null)
                    {
                        _timer = new TimerData(DELAY_BEFOR_FIRE, _timerController);
                    }

                    _isTimerOver = _timer.IsTimerEndStatus;

                    if (_isTimerOver)
                    {
                        _activeEnemy.IsYourTurn = true;
                        _isTimerOver = false;
                        _timer = null;
                    }
                }
                else
                {
                    EndTurn();
                }
            }

        }

        private bool IsAllPlayersDie()
        {
            var player = _unitStorage.Players.Find(player => player.AliveStateController.State.IsAlive);
            if (player is null) return true;
            else return false;
        }

        private void AddDeadEnemy(IGamer enemy)
        {
            if (!enemy.IsShoted)
            {
                _shootedOrDeadEnemies++;
            }
        }

        private void EndTurn()
        {
            _globalTurnCount++;
            _turnCountText.text = String.Concat("Ход ", _globalTurnCount);

            _elementsController.UpdateElements();

            foreach (var gamer in _queueGamers)
            {
                gamer.IsShoted = false;
            }
            endGlobalTurn.Invoke();
            Debug.Log("EndTurn");
        }

        void ILoadeble.Load(IMementoData mementoData)
        {
            if (mementoData is TurnMementoData turnMemento)
            {
                _globalTurnCount = turnMemento.turnCount;
                _turnCountText.text = String.Concat("Ход ", _globalTurnCount);
                _shootedOrDeadEnemies = turnMemento.shootedOrDeadEnemies;
            }
            else
            {
                throw new Exception("Передан не тот mementoData");
            }
        }
    }
}