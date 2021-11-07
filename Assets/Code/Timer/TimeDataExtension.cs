using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MVC
{
    public class TimeDataExtension: IDisposable
    {

        public event Action OnTimerEndWithBool = delegate () { };

        private bool _isTimerEnd;

        public TimeDataExtension()
        {
            _isTimerEnd = false;
            OnTimerEndWithBool += ChangeTimerStatusExtension;
        }

        private void ChangeTimerStatusExtension()
        {
            _isTimerEnd = true;
        }

        public void InvokeTimerEndWithBool()
        {
            if (!_isTimerEnd)
            {
                OnTimerEndWithBool.Invoke();
            }
            
        }

        public virtual void Dispose()
        {
            OnTimerEndWithBool -= ChangeTimerStatusExtension;
        }
    }
}

