using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class LoadHandler
    {

        private List<ICommand> _loaders;

        public LoadHandler(GameResetOrEndManager gameResetOrEndManager, PlayerAbilityController playerAbilityController, TurnController turnController, UnitStorage unitStorage)
        {
            _loaders = new List<ICommand>();
            _loaders.Add(new LoadGameResetOrEndManager(gameResetOrEndManager));
            _loaders.Add(new LoadUnitStorage(unitStorage));
            _loaders.Add(new LoadTurnController(turnController));
            _loaders.Add(new LoadPlayerAbilityController(playerAbilityController));
            
        }

        public void Load(GameMemento savedData)
        {
            foreach(var loader in _loaders)
            {
                loader.Load(savedData);
            }
        }
    }
}



