using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace MVC
{
    public class TimeData : TimeDataExtension
    {

        public event Action OnTimerEnd = delegate () { };

        private bool IsTimerEnd;

        private readonly float _startTime;
        private readonly float _deltaTime;

        public bool IsTimerEndStatus { get => IsTimerEnd; }
        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }

        public TimeData(float deltaTime)
        {
            OnTimerEnd += ChangeTimerStatus;
            IsTimerEnd = false;
            _startTime = Time.time;
            _deltaTime = deltaTime;
        }

        private void ChangeTimerStatus()
        {

            IsTimerEnd = true;
        }

        public void InvokeTimerEnd()
        {
                OnTimerEnd.Invoke();
        }

        public override void Dispose()
        {
            base.Dispose();
            OnTimerEnd -= ChangeTimerStatus;
        }
    }
}

