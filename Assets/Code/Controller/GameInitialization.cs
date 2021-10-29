namespace MVC
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, IPlayerTurn[] players)
        {
            controllers.Add(new TurnController(players));
        }
    }
}