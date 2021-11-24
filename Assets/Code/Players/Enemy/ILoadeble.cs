namespace MVC
{
    public interface ILoadeble
    {
        public void Load<T>(T mementoData) where T : IMementoData;
    }
}