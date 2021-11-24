using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class LoadTurnController : ICommand
    {
        public bool Succeeded { get; private set; }
        private TurnController _turnController;

        public LoadTurnController(TurnController turnController)
        {
            _turnController = turnController;
        }

        public bool Load(GameMemento savedData)
        {
            _turnController.GlobalTurnCount = savedData.turnCount;
            _turnController.UpdateCountText();
            Succeeded = true;
            return Succeeded;
        }
    }
}

