using System.Collections.Generic;

namespace MVC
{
    internal sealed class TurnController : IExecute
    {

        private Queue<IGamer> _queueGamers;
        private float _currentTimer;
        private float _delayBeforeFire;

        public TurnController(IEnumerable<IGamer> gamersList, float delayBeforeFire)
        {
            _queueGamers = new Queue<IGamer>(gamersList);
            _delayBeforeFire = delayBeforeFire;
            _currentTimer = 0f;
        }

        public void Execute(float deltaTime)
        {
            var currentPlayer = _queueGamers.Peek();
            if (currentPlayer.IsDead)
            {
                currentPlayer.IsYourTurn = false;
            }
            if (!currentPlayer.IsYourTurn)
            {
                if(Timer(_delayBeforeFire, deltaTime))
                {
                    PassNext();
                }
            }
        }

        private bool Timer(float seconds, float deltaTime)
        {
            if(_currentTimer < seconds)
            {
                _currentTimer += deltaTime;
                return false;
            }
            else
            {
                _currentTimer = 0f;
                return true;
            }
        }

        private void PassNext()
        {
            var currentPlayer = _queueGamers.Dequeue();
            _queueGamers.Enqueue(currentPlayer);
            _queueGamers.Peek().IsYourTurn = true;
        }
    }
}