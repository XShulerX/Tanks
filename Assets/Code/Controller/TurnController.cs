using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public sealed class TurnController : IExecute
    {
        public Action endGlobalTurn = delegate () { };

        private UnitStorage _unitStorage;

        private LinkedList<IGamer> _queueGamers;
        private bool _isTimerOver;
        private IGamer _player;
        private TimerController _timerController;
        private ElementsController _elementsController;
        private TimerData _timer;
        private Text _text;

        private int _shotedOrDeadEnemies;
        private int _enemiesCount;
        private int _globalTurnCount = 1;
        private bool _isControllerInReset;

        private const float DELAY_BEFOR_FIRE = 1f;


        public TurnController(UnitStorage unitStorage, TimerController timerController, ElementsController elementsController, Text text)
        {
            _unitStorage = unitStorage;

            _elementsController = elementsController;
            _queueGamers = new LinkedList<IGamer>(unitStorage.gamers); // Перешел с очереди на Линкед лист для возможности перестановки
            _timerController = timerController;
            _player = unitStorage.gamers[0];
            for (int i = 1; i < unitStorage.gamers.Count; i++)
            {
                _enemiesCount++;
                unitStorage.gamers[i].wasKilled += AddDeadEnemy;
            }
            _text = text;
            _text.text = "Ход 1";
        }

        public void ResetTurns()
        {
            _enemiesCount = 0;
            _globalTurnCount = 0;
            _shotedOrDeadEnemies = 0;

            _queueGamers.Clear();
            for (int i = 0; i < _unitStorage.gamers.Count; i++)
            {
                _queueGamers.AddLast(_unitStorage.gamers[i]);
            }

            _player = _unitStorage.gamers[0];
            for (int i = 1; i < _unitStorage.gamers.Count; i++)
            {
                _enemiesCount++;
                _unitStorage.gamers[i].wasKilled += AddDeadEnemy;
            }
        }

        public void Execute(float deltaTime)
        {
            if (_player.IsDead) return;

            var currentPlayer = _queueGamers.First.Value;
            if (currentPlayer.IsDead)
            {
                currentPlayer.IsYourTurn = false;
                PassNext();
            }
            if (!currentPlayer.IsYourTurn)
            {
                if (_timer is null)
                {
                    _timer = new TimerData(DELAY_BEFOR_FIRE, _timerController);
                }

                _isTimerOver = _timer.IsTimerEndStatus;

                if (_isTimerOver)
                {
                    PassNext();
                    _isTimerOver = false;
                    _timer = null;
                }
            }
        }

        private void AddDeadEnemy(IGamer enemy)
        {
            if (!enemy.IsShoted)
            {
                _shotedOrDeadEnemies++;
            }
        }

        private void EndTurn()
        {
            endGlobalTurn.Invoke();
            _globalTurnCount++;
            _text.text = "Ход " + _globalTurnCount;

            _queueGamers.Remove(_player);
            _queueGamers.AddFirst(_player);

            foreach (var enemy in _queueGamers)
            {
                enemy.IsShoted = false;
                if (enemy != _player && !enemy.IsDead)
                {
                    _shotedOrDeadEnemies--;
                }
            }
            
            Debug.Log("EndTurn");
            _elementsController.UpdateElements();
        }

        private void PassNext()
        {
            var currentPlayer = _queueGamers.First.Value;
            _queueGamers.RemoveFirst();
            _queueGamers.AddLast(currentPlayer);

            if (currentPlayer != _player && !currentPlayer.IsDead)
            {
                _shotedOrDeadEnemies++;
            }

            if (_shotedOrDeadEnemies == _enemiesCount)
            {
                EndTurn();
            }

            _queueGamers.First.Value.IsYourTurn = true;
        }
    }
}