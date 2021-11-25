using System;
using System.Collections.Generic;


namespace MVC
{
    public class LoadHandler
    {
        public Action<bool> isOnLoad = delegate (bool b) { };
        private List<ICommand> _loaders;
        private TimerController _timerController;

        public LoadHandler(GameResetOrEndManager gameResetOrEndManager, PlayerAbilityController playerAbilityController, TurnController turnController, UnitStorage unitStorage, TimerController timerController)
        {
            _loaders = new List<ICommand>();
            _timerController = timerController;
            _loaders.Add(new LoadGameResetOrEndManager(gameResetOrEndManager));
            _loaders.Add(new LoadUnitStorage(unitStorage));
            _loaders.Add(new LoadTurnController(turnController));
            _loaders.Add(new LoadPlayerAbilityController(playerAbilityController));
            
        }

        public void Load(GameMemento savedData)
        {
            isOnLoad.Invoke(true);
            foreach (var loader in _loaders)
            {
                loader.Load(savedData);
            }

            var timer = new TimerData(1f, _timerController);
            timer.TimerIsOver += EndLoad;
        }

        private void EndLoad()
        {
            isOnLoad.Invoke(false);
        }
    }
}



