using System;

namespace MVC
{
    class TimerWhithParameters<T>: TimerDecorator
    {
        public event Action<T> TimerIsOver = delegate (T parameter) { };
        
        private T _parameter;
        public TimerWhithParameters(TimerData timerData, T parameter): base(timerData)
        {
            _parameter = parameter;
            _timerData.TimerIsOver += BaseTimerIsOver;
        }

        protected override void BaseTimerIsOver()
        {
            TimerIsOver.Invoke(_parameter);
        }
    }

    class TimerWhithParameters<T,T2> : TimerDecorator
    {
        public event Action<T, T2> TimerIsOver = delegate (T p1, T2 p2) { };

        private T _firstParameter;
        private T2 _secomdParameter;
        public TimerWhithParameters(TimerData timerData, T firstParameter, T2 secondParameter) : base(timerData)
        {
            _firstParameter = firstParameter;
            _secomdParameter = secondParameter;
            _timerData.TimerIsOver += BaseTimerIsOver;
        }

        protected override void BaseTimerIsOver()
        {
            TimerIsOver.Invoke(_firstParameter, _secomdParameter);
        }
    }

    class TimerWhithParameters<T, T2, T3> : TimerDecorator
    {
        public event Action<T, T2, T3> TimerIsOver = delegate (T p1, T2 p2, T3 p3) { };

        private T _firstParameter;
        private T2 _secomdParameter;
        private T3 _thirdParameter;

        public TimerWhithParameters(TimerData timerData, T firstParameter, T2 secondParameter, T3 thirdParameter) : base(timerData)
        {
            _firstParameter = firstParameter;
            _secomdParameter = secondParameter;
            _thirdParameter = thirdParameter;
            _timerData.TimerIsOver += BaseTimerIsOver;
        }

        protected override void BaseTimerIsOver()
        {
            TimerIsOver.Invoke(_firstParameter, _secomdParameter, _thirdParameter);
        }
    }
}
