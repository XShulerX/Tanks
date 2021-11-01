using System;
using UnityEngine;

namespace MVC
{
    public sealed class Player : MonoBehaviour, IPlayerTurn
    {
        public bool isYourTurn { get ; set; }
        public event Action OnCollisionEnterChange;

        public Player()
        {
            isYourTurn = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterChange?.Invoke();
        }
    }
}