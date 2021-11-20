using System;

namespace MVC
{
    public class GameResetManager
    {
        public event Action<bool> sceneResetState = delegate (bool b) { };
        public event Action gameOver = delegate () { };
        public event Action restartGame = delegate () { };

        private PlayerAbilityController _playerAbilityController;
        private UnitController _unitController;
        private BulletPool _bulletPool;
        private ElementsController _elementsController;
        private TurnController _turnController;
        private UnitStorage _unitStorage;
        private int _attemptsCount;

        private const int MAX_ATTEMPTS_COUNT = 3;

        public GameResetManager(UnitController unitController, BulletPool bulletPool, ElementsController elementsController, Controllers controllers, 
                                TurnController turnController, UnitStorage unitStorage, PlayerAbilityController playerAbilityController)
        {
            _playerAbilityController = playerAbilityController;
            _unitController = unitController;
            _unitStorage = unitStorage;
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
                RestartGame();
            }

        }

        private void RestartGame()
        {
            sceneResetState.Invoke(true);
            restartGame.Invoke();
        }

        public void PlayerWin()
        {
            _unitController.IncreaseForceModifer();
            sceneResetState.Invoke(true);
            ResetScene();
            sceneResetState.Invoke(false);
        }

        public void ResetScene() // todo - сделать вызов ресета у таймера, для сброса всех таймеров на момент сброса уровня
        {
            
            _playerAbilityController.ResetAbilities();
            _unitController.ResetEnemies();
            _unitController.ResetPlayer();
            _turnController.ResetTurns();
            _elementsController.UpdateElements();
            _bulletPool.ReturnAndResetAllBullets();
            sceneResetState.Invoke(false);
        }

        private void GameOver()
        {
            gameOver.Invoke();
        }
    }
}