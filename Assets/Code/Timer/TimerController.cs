using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TimerController : IExecute
    {
        private List<TimeData> _timers = new List<TimeData>();

        private const float REQUIRED_FOR_DELETING_TIMER_TIME = 60f;


        public void AddTimer(TimeData timeData)
        {
            _timers.Add(timeData);
        }

        public void RemoveTimeData(TimeData timeData)
        { 
            _timers.Remove(timeData);
            
        }

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < _timers.Count; i++)
            {
                if ((Time.time - _timers[i].GetStartTime) >= _timers[i].GetDeltaTime)
                {
                    _timers[i].InvokeTimerEnd();

                    if((Time.time - _timers[i].GetStartTime) >= REQUIRED_FOR_DELETING_TIMER_TIME)
                    {
                        RemoveTimeData(_timers[i]);
                    }
                }
            }
        }
    }
}

