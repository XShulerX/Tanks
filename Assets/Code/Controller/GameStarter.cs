using UnityEngine;
using System.Collections.Generic;

namespace MVC
{
    public sealed class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;
        [SerializeField]
        private Player player;

        private Controllers _controllers;

        private void Start()
        {
            IPlayerTurn[] players = new IPlayerTurn[2] {enemy, player};
            _controllers = new Controllers();
            new GameInitialization(_controllers, players);
            _controllers.Initilazation();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void FixedUpdate()
        {
            _controllers.PhysicsExecute();
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}
