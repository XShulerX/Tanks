using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField]
        private Text _text;

        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            new GameInitialization(_controllers, _enemyData, player, _box, _text);
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
