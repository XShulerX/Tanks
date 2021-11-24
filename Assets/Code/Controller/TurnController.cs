using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public sealed class TurnController : IExecute, IResetable
    {
        public Action endGlobalTurn = delegate () { };

        private UnitStorage _unitStorage;

        private LinkedList<IGamer> _queueGamers;
        private bool _isTimerOver;
        private IGamer _player;
        private TimerController _timerController;
        private ElementsController _elementsController;
        private TimerData _timer;
        private Text _turnCountText;

        private int _shotedOrDeadEnemies;
        private int _enemiesCount;
        private int _globalTurnCount = 1;

        private const float DELAY_BEFOR_FIRE = 1f;

        public int GlobalTurnCount { get => _globalTurnCount; set => _globalTurnCount = value; }

        public TurnController(UnitStorage unitStorage, TimerController timerController, ElementsController elementsController, Text uiTurnCountText)
        {
            _unitStorage = unitStorage;

            _elementsController = elementsController;
            _queueGamers = new LinkedList<IGamer>(unitStorage.Gamers); // ������� � ������� �� ������ ���� ��� ����������� ������������
            _timerController = timerController;
            _player = unitStorage.Gamers[0];
            for (int i = 1; i < unitStorage.Gamers.Count; i++)
            {
                _enemiesCount++;
                unitStorage.Gamers[i].wasKilled += AddDeadEnemy;
            }
            _turnCountText = uiTurnCountText;
            _turnCountText.text = "��� 1";
        }

        public void Reset()
        {
            _enemiesCount = 0;
            _globalTurnCount = 1;
            _turnCountText.text = "��� 1";
            _shotedOrDeadEnemies = 0;
            _timer = null;

            _queueGamers.Clear();
            for (int i = 0; i < _unitStorage.Gamers.Count; i++)
            {
                _queueGamers.AddLast(_unitStorage.Gamers[i]);
                if (_unitStorage.Gamers[i] is Enemy)
                {
                    _enemiesCount++;
                }
            }
            _player = _unitStorage.Gamers[0];
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
            _globalTurnCount++;
            _turnCountText.text = String.Concat("��� ", _globalTurnCount);

            _queueGamers.Remove(_player);
            _queueGamers.AddFirst(_player);
            _elementsController.UpdateElements();

            foreach (var gamer in _queueGamers)
            {
                gamer.IsShoted = false;
                if (gamer != _player && !gamer.IsDead)
                {
                    _shotedOrDeadEnemies--;
                }
            }
            endGlobalTurn.Invoke();
            Debug.Log("EndTurn");
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