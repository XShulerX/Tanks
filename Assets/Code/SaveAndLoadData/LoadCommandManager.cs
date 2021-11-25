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
            _abilityLoadCommand = new AbilityLoadCommand(playerAbilityController);
            _timerController = timerController;
        }

        public void Load(GameMemento savedData)
        {
            isOnLoad.Invoke(true);
            _stageLoadCommand.Load(savedData.stageMemento);
            _turnControllerLoadCommand.Load(savedData.turnMemento);
            _abilityLoadCommand.Load(savedData.abilitiesMemento);
            _unitLoadCommand.Load(savedData.playerMemento, savedData.enemiesMementos);

            var timer = new TimerData(1f, _timerController);
            timer.TimerIsOver += EndLoad;
        }

        private void EndLoad()
        {
            isOnLoad.Invoke(false);
        }
    }
}