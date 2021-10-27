using UnityEngine;

namespace MVC
{
    public sealed class GameStarter : MonoBehaviour
    {
        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initilazation();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}
