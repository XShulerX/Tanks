namespace MVC
{
    public class StageLoadCommand
    {
        public bool Succeeded { get; private set; }
        private ILoadeble _gameResetOrEndManager;

        public StageLoadCommand(GameResetOrEndManager gameResetOrEndManager)
        {
            _gameResetOrEndManager = gameResetOrEndManager;
        }

        public bool Load(StageMementoData mementoData)
        {
            _gameResetOrEndManager.Load<StageMementoData>(mementoData);
            Succeeded = true;
            return Succeeded;
        }
    }
}