namespace MVC
{
    public class TurnControllerLoadCommand
    {
        public bool Succeeded { get; private set; }
        private ILoadeble _turnController;

        public TurnControllerLoadCommand(TurnController turnController)
        {
            _turnController = turnController;
        }

        public bool Load(TurnMementoData mementoData)
        {
            _turnController.Load<TurnMementoData>(mementoData);
            Succeeded = true;
            return Succeeded;
        }
    }
}