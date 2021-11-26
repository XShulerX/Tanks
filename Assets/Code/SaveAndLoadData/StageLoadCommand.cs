namespace MVC
{
    public class StageLoadCommand: ILoadCommand
    {
        public bool Succeeded { get; private set; }
        private ILoadeble _gameResetOrEndManager;

        public StageLoadCommand(GameResetOrEndManager gameResetOrEndManager)
        {
            _gameResetOrEndManager = gameResetOrEndManager;
        }

        public bool Load(IMementoData mementoData)
        {
            _gameResetOrEndManager.Load(mementoData);
            Succeeded = true;
            return Succeeded;
        }
    }
}