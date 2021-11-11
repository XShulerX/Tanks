using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MVC
{
    public class TimeDataExtension: IDisposable
    {

        public event Action<GameObject> OnTimerEndWithBool = delegate (GameObject gameObject) { };

        private bool _isTimerEnd;
        private GameObject _gameObject;

        public TimeDataExtension(GameObject gameObject)
        {
            _gameObject = gameObject;
            _isTimerEnd = false;
            OnTimerEndWithBool += ChangeTimerStatusExtension;
        }

        private void ChangeTimerStatusExtension(GameObject gameObject)
        {
            _isTimerEnd = true;
        }

        public void InvokeTimerEndWithBool()
        {
            if (!_isTimerEnd)
            {
                OnTimerEndWithBool.Invoke(_gameObject);
            }
            
        }

        public virtual void Dispose()
        {
            OnTimerEndWithBool -= ChangeTimerStatusExtension;
        }
    }
}

