using UnityEngine;

namespace MVC
{
    public sealed class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private EnemyData _enemyData;
        [SerializeField]
        private Player player;
        [SerializeField]
        private GameObject _box;

        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            new GameInitialization(_controllers, _enemyData, player, _box);
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
