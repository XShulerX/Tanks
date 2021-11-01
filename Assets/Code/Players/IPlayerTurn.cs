using System;

namespace MVC
{
    public interface IPlayerTurn
    {
        event Action OnCollisionEnterChange;
        bool isYourTurn { get; set; }
    }
}