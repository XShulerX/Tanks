using System;

namespace MVC
{
    public class GameResetOrEndManager: ILoadeble
    {
        public event Action<bool> sceneResetState = delegate (bool b) { };
        public event Action gameOver = delegate () { };
        public event Action<int> lostGame = delegate (int t) { };
        public event Action<int> winGame = delegate (int t) { };

        private UnitCrateAndResetController _unitController;
        private Controllers _controllers;
        private int _attemptsCount = 1;
        private int _stageCount;
        private bool _isAttemptOver; //todo - придумать более изящную проверку на наличие уже вызванного
                                     //окна и завершения попытки, либо уточнить у Евгения как лучше сделать

        private const int MAX_ATTEMPTS_COUNT = 3;

        public int AttemptsCount { get => _attemptsCount; }
        public int StageCount { get => _stageCount; }
        public UnitCrateAndResetController UnitController { get => _unitController; }

        public GameResetOrEndManager(UnitCrateAndResetController unitController, Controllers controllers)
        {
            _controllers = controllers;
            _unitController = unitController;
            _controllers.SignOnResetController(this);
        }

        public void PlayerLost()
        {
            if (_isAttemptOver) return;
            else _isAttemptOver = true;

            if (_attemptsCount == MAX_ATTEMPTS_COUNT)
            {
                GameOver();
            }
            else
            {
                RestartLostGame();
            }

        }

        private void RestartLostGame()
        {
            _attemptsCount++;
            lostGame.Invoke(1 + MAX_ATTEMPTS_COUNT - _attemptsCount);
        }

        public void PlayerWin()
        {
            if (_isAttemptOver) return;
            else _isAttemptOver = true;

            _stageCount++;
            _unitController.IncreaseForceModifer();
            winGame.Invoke(_stageCount);
        }

        public void ResetScene()
        {
            sceneResetState.Invoke(true);
            _controllers.Reset();
            sceneResetState.Invoke(false);
            _isAttemptOver = false;
        }

        private void GameOver()
        {
            gameOver.Invoke();
        }

        void ILoadeble.Load<T>(T mementoData)
        {
            var stageMemento = mementoData as StageMementoData;

            ResetScene();
            _attemptsCount = stageMemento.attemptsCount;
            _unitController.SetForceModifier(stageMemento.forceModifer);
            _stageCount = stageMemento.stageCount;
        }
    }
}