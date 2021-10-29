using UnityEngine;

namespace MVC
{

    public class Enemy : MonoBehaviour, IPlayerTurn
    {
        public bool isYourTurn { get ; set; }

        public Enemy()
        {
            isYourTurn = false;
        }
    }
}