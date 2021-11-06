using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public sealed class TurnController : IExecute
    {
        public Action endGlobalTurn = delegate () { };

        private LinkedList<IGamer> _queueGamers;
        private bool _isTimerOver;
        private IGamer _player;
        private TimerController _timerController;
        private TimeData _timer;
        private int _maxTurnCount;

        private int _turnCount = 1;
        private const float DELAY_BEFOR_FIRE = 1f;

        public TurnController(List<IGamer> gamersList, TimerController timerController)
        {
            _queueGamers = new LinkedList<IGamer>(gamersList); // ѕерешел с очереди на Ћинкед лист дл€ возможности перестановки
            _timerController = timerController;
            _player = gamersList[0];
            _maxTurnCount = (gamersList.Count - 1) * 2;

        }

        public void Execute(float deltaTime)
        {
            var currentPlayer = _queueGamers.First.Value;
            if (currentPlayer.IsDead)
            {
                currentPlayer.IsYourTurn = false;
            }
            if (!currentPlayer.IsYourTurn)
            {
                if (_timer is null)
                {
                    _timer = new TimeData(DELAY_BEFOR_FIRE);
                    _timerController.AddTimer(_timer);
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

        //private bool Timer(float seconds, float deltaTime)
        //{
        //    if(_currentTimer < seconds)
        //    {
        //        _currentTimer += deltaTime;
        //        return false;
        //    }
        //    else
        //    {
        //        _currentTimer = 0f;
        //        return true;
        //    }
        //}

        private void PassNext()
        {
            var currentPlayer = _queueGamers.First.Value;
            _queueGamers.RemoveFirst();
            
            if (!currentPlayer.IsDead) // ≈сли мертв, не возвращаем в очередь
            {
                _queueGamers.AddLast(currentPlayer);
                _turnCount++;
            } else
            {
                _maxTurnCount -= 2;
            }          
            if (_turnCount % 2 != 0) //  аждый не четный шаг стрел€ет игрок
            {
                _queueGamers.Remove(_player);
                _queueGamers.AddFirst(_player);
            }

            if (_turnCount == _maxTurnCount) // ≈сли сделано 6 выстрелов - глобальный ход заверешн
            {
                endGlobalTurn.Invoke();
                _turnCount = 0;
                Debug.Log("EndTurn");
            }
            _queueGamers.First.Value.IsYourTurn = true;
        }
    }
}