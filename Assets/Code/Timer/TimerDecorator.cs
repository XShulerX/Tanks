
namespace MVC
{
    public abstract class TimerDecorator
    {
        protected TimerData _timerData;

        protected TimerDecorator(TimerData timeData)
        {
            _timerData = timeData;
        }

        protected abstract void BaseTimerIsOver();
    }
}
