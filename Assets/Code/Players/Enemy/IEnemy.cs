using UnityEngine;

namespace MVC
{
    public interface IEnemy : IPlayerTurn
    {
        void Fire(Transform target);
    }
}