namespace MVC
{
    public interface ILateExecute : IController
    {
        void LateExecute(float deltaTime);
    }
}