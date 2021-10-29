using System.Collections.Generic;

namespace MVC
{
    internal sealed class TurnController : IExecute
    {

        private Queue<IPlayerTurn> _queueGamers;
        private float _delay;

        public TurnController(IPlayerTurn[] gamersList)
        {
            _queueGamers = new Queue<IPlayerTurn>(gamersList);
        }

        public void Execute(float deltaTime)
        {
            var currentPlayer = _queueGamers.Peek();
            if (!currentPlayer.isYourTurn)
            {
                PassNext();
            }
        }

        private void PassNext()
        {
            var currentPlayer = _queueGamers.Dequeue();
            _queueGamers.Enqueue(currentPlayer);
            _queueGamers.Peek().isYourTurn = true;
        }

        private void EndTurn()
        {

        }
    }
}