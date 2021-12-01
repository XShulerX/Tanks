using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public sealed class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private EnemyData _enemyData;
        [SerializeField]
        private AbilitiesData _abilitiesData;
        [SerializeField]
        private List<Player> players;
        [SerializeField]
        private GameObject _box;
        [SerializeField]
        private UIInitializationModel _uiInitModel;

        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            new GameInitialization(_controllers, _enemyData, players, _box, _uiInitModel, _abilitiesData);
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
