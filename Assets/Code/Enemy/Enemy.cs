using UnityEngine;

namespace MVC
{

    public class Enemy : MonoBehaviour, IEnemy
    {
        public bool isYourTurn { get ; set; }

        private void Start()
        {
            isYourTurn = false;
        }

        private void Fire()
        {

        }
    }
}