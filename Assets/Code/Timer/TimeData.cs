using UnityEngine;
using System;


namespace MVC
{
    public class TimeData
    {

        public event Action TimerEnd = delegate () { };
        public event Action<IGamer> TimerEndWithGamer = delegate (IGamer gamer) { };

        private bool IsTimerEnd;
        private IGamer _gamer;

        private readonly float _startTime;
        private readonly float _deltaTime;

        public bool IsTimerEndStatus { get => IsTimerEnd; }
        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }

        public TimeData(float deltaTime, TimerController timerController)
        {
            TimerEnd += ChangeTimerStatus;
            IsTimerEnd = false;
            _startTime = Time.time;
            _deltaTime = deltaTime;

            timerController.AddTimer(this);
        }

        public TimeData(float deltaTime, IGamer gamer, TimerController timerController)
        {
            TimerEnd += ChangeTimerStatus;
            IsTimerEnd = false;
            _startTime = Time.time;
            _deltaTime = deltaTime;
            _gamer = gamer;

            timerController.AddTimer(this);
        }

        private void ChangeTimerStatus()
        {
            IsTimerEnd = true;
        }

        public void InvokeTimerEnd()
        {
            TimerEnd.Invoke();
            TimerEndWithGamer.Invoke(_gamer);
        }

        public void Dispose()
        {
            TimerEnd -= ChangeTimerStatus;
        }
    }
}

