using UnityEngine;
using System;


namespace MVC
{
    public class TimerData
    {

        public event Action TimerIsOver = delegate () { };

        private bool IsTimerEnd;
        private IGamer _gamer;

        private readonly float _startTime;
        private readonly float _deltaTime;

        public bool IsTimerEndStatus { get => IsTimerEnd; }
        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }

        public TimerData(float deltaTime, TimerController timerController)
        {
            TimerIsOver += ChangeTimerStatus;
            IsTimerEnd = false;
            _startTime = Time.time;
            _deltaTime = deltaTime;

            timerController.AddTimer(this);
        }

        private void ChangeTimerStatus()
        {
            IsTimerEnd = true;
        }

        public void InvokeTimerEnd()
        {
            TimerIsOver.Invoke();
        }

        public void Dispose()
        {
            TimerIsOver -= ChangeTimerStatus;
        }
    }
}

