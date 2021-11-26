namespace MVC
{
    public class TurnControllerLoadCommand: ILoadCommand
    {
        public bool Succeeded { get; private set; }
        private ILoadeble _turnController;

        public TurnControllerLoadCommand(TurnController turnController)
        {
            _turnController = turnController;
        }

        public bool Load(IMementoData mementoData)
        {
            _turnController.Load(mementoData);
            Succeeded = true;
            return Succeeded;
        }
    }
}