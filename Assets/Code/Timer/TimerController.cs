using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TimerController : IExecute
    {
        private List<TimerData> _timers = new List<TimerData>();

        private const float REQUIRED_FOR_DELETING_TIMER_TIME = 20f;


        public void AddTimer(TimerData timeData)
        {
            _timers.Add(timeData);
        }

        public void RemoveTimeData(TimerData timeData)
        { 
            _timers.Remove(timeData);
            
        }

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < _timers.Count; i++)
            {
                if ((Time.time - _timers[i].GetStartTime) >= _timers[i].GetDeltaTime && !_timers[i].IsTimerEndStatus)
                {
                    _timers[i].InvokeTimerEnd();
                }

                if ((Time.time - _timers[i].GetStartTime) >= REQUIRED_FOR_DELETING_TIMER_TIME)
                {
                    RemoveTimeData(_timers[i]);
                }
            }
        }
    }
}

