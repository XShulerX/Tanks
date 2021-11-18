using System;

namespace MVC
{
    public class GameResetManager
    {
        public event Action<bool> sceneResetState = delegate (bool b) { };

        private UnitController _unitController;
        private BulletPool _bulletPool;
        private ElementsController _elementsController;
        private TurnController _turnController;
        private int _attemptsCount;

        private const int MAX_ATTEMPTS_COUNT = 3;

        public GameResetManager(UnitController unitController, BulletPool bulletPool, ElementsController elementsController, Controllers controllers, TurnController turnController)
        {
            _unitController = unitController;
            _bulletPool = bulletPool;
            _elementsController = elementsController;
            _turnController = turnController;
            controllers.SignOnResetController(this);
        }

        public void PlayerLost()
        {
            if (_attemptsCount == MAX_ATTEMPTS_COUNT)
            {
                GameOver();
            }
            else
            {
                _attemptsCount++;
                sceneResetState.Invoke(true);
                ResetScene();
                sceneResetState.Invoke(false);
            }

        }

        public void PlayerWin()
        {
            _unitController.IncreaseForceModifer();
            sceneResetState.Invoke(true);
            ResetScene();
            sceneResetState.Invoke(false);
        }

        public void ResetScene()
        {
            _unitController.ResetPlayer();
            _unitController.ResetEnemies();
            _turnController.ResetTurns();
            _elementsController.UpdateElements();
            _bulletPool.ReturnAndResetAllBullets();
        }

        private void GameOver()
        {
            throw new NotImplementedException();
        }
    }
}