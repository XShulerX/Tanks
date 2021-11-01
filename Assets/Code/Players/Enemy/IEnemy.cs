using UnityEngine;

namespace MVC
{
    public interface IEnemy : IPlayerTurn, IPlayerTarget
    {
        void Fire(Transform target);
    }
}