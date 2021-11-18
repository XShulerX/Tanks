using System.Collections.Generic;

namespace MVC
{
    internal sealed class Controllers : ICleanup, IExecute, IInitialization, IPhysicsExecute
    {
        private readonly List<ICleanup> _cleanupControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IPhysicsExecute> _physicsExecuteControllers;

        internal Controllers()
        {
            _cleanupControllers = new List<ICleanup>();
            _executeControllers = new List<IExecute>();
            _initializeControllers = new List<IInitialization>();
            _physicsExecuteControllers = new List<IPhysicsExecute>();
        }

        internal Controllers Add(IController controller)
        {
            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }

            if (controller is IExecute executeController)
            {
                _executeControllers.Add(executeController);
            }

            if (controller is IInitialization initializationController)
            {
                _initializeControllers.Add(initializationController);
            }

            if (controller is IPhysicsExecute physicsExecuteController)
            {
                _physicsExecuteControllers.Add(physicsExecuteController);
            }

            return this;
        }

        public void Cleanup()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index)
            {
                _cleanupControllers[index].Cleanup();
            }
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                _executeControllers[index].Execute(deltaTime);
            }
        }

        public void Initilazation()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Initilazation();
            }
        }

        public void PhysicsExecute()
        {
            for (var index = 0; index < _physicsExecuteControllers.Count; ++index)
            {
                _physicsExecuteControllers[index].PhysicsExecute();
            }
        }
    }
}