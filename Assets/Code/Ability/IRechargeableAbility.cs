namespace MVC
{
    public interface IRechargeableAbility
    {
        public bool IsOnCooldown { get; }
        public Elements ElementType { get; }
    }
}