using System;

namespace MVC
{
    public class LoadCommandManager
    {
        public Action<bool> isOnLoad = delegate (bool b) { };

        private StageLoadCommand _stageLoadCommand;
        private UnitLoadCommand _unitLoadCommand;
        private TurnControllerLoadCommand _turnControllerLoadCommand;
        private AbilityLoadCommand _abilityLoadCommand;

        private TimerController _timerController;

        public LoadCommandManager(GameResetOrEndManager gameResetOrEndManager, PlayerAbilityController playerAbilityController, TurnController turnController, UnitStorage unitStorage, TimerController timerController)
        {
            _stageLoadCommand =  new StageLoadCommand(gameResetOrEndManager);
            _unitLoadCommand = new UnitLoadCommand(unitStorage);
            _turnControllerLoadCommand = new TurnControllerLoadCommand(turnController);
            _abilityLoadCommand = new AbilityLoadCommand(unitStorage);
            _timerController = timerController;
        }

        public void Load(GameMemento savedData)
        {
            isOnLoad.Invoke(true);
            _stageLoadCommand.Load(savedData.stageMemento);
            _turnControllerLoadCommand.Load(savedData.turnMemento);

            foreach(var abilityMemento in savedData.abilitiesMemento)
            {
                _abilityLoadCommand.Load(abilityMemento);
            }

            foreach (var enemyMemento in savedData.enemiesMementos)
            {
                _unitLoadCommand.Load(enemyMemento);
            }

            foreach (var playerMemento in savedData.playersMementos)
            {
                _unitLoadCommand.Load(playerMemento);
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