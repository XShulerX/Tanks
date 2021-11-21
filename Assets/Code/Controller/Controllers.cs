using System.Collections.Generic;

namespace MVC
{
    public sealed class Controllers : ICleanup, IExecute, IInitialization, IPhysicsExecute
    {
        private readonly List<ICleanup> _cleanupControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IResetable> _resetebleControllers;
        private readonly List<IPhysicsExecute> _physicsExecuteControllers;

        private GameResetManager _resetController;
        private bool _isReset;

        internal Controllers()
        {
            _cleanupControllers = new List<ICleanup>();
            _executeControllers = new List<IExecute>();
            _initializeControllers = new List<IInitialization>();
            _resetebleControllers = new List<IResetable>();
            _physicsExecuteControllers = new List<IPhysicsExecute>();
        }

        public Controllers Add(IController controller)
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

            if (controller is IResetable resetebleController)
            {
                _resetebleControllers.Add(resetebleController);
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
            _resetController.sceneResetState -= ChangeResetState;
        }

        public void Execute(float deltaTime)
        {
            if (_isReset) return;
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

        public void Reset()
        {
            for (var index = 0; index < _resetebleControllers.Count; ++index)
            {
                _resetebleControllers[index].Reset();
            }
        }

        public void PhysicsExecute()
        {
            if (_isReset) return;
            for (var index = 0; index < _physicsExecuteControllers.Count; ++index)
            {
                _physicsExecuteControllers[index].PhysicsExecute();
            }
        }

        public void SignOnResetController(GameResetManager resetController)
        {
            _resetController = resetController;
            _resetController.sceneResetState += ChangeResetState;
        }

        private void ChangeResetState(bool isReset)
        {
            _isReset = isReset;
        }
    }
}