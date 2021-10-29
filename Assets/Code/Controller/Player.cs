using UnityEngine;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayerTurn
    {
        public bool isYourTurn { get ; set; }

        public Player()
        {
            isYourTurn = true;
        }
    }
}