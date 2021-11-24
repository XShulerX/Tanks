using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class LoadGameResetOrEndManager : ICommand
    {
        public bool Succeeded { get; private set; }
        private GameResetOrEndManager _gameResetOrEndManager;

        public LoadGameResetOrEndManager(GameResetOrEndManager gameResetOrEndManager)
        {
            _gameResetOrEndManager = gameResetOrEndManager;
        }

        public bool Load(GameMemento savedData)
        {
            _gameResetOrEndManager.ResetScene();
            _gameResetOrEndManager.AttemptsCount = savedData.attemptsCount;
            _gameResetOrEndManager.UnitController.ForceModifer = savedData.forceModifier;
            _gameResetOrEndManager.StageCount = savedData.stageCount;
            Succeeded = true;
            return Succeeded;
        }
    }
}


