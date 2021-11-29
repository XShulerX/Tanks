using System;

namespace MVC
{
    public class TankDestroyingController
    {
        public event Action playerTankWasDestroed = delegate () { };
        public event Action allEnemyTanksWasDestroed = delegate () { };

        private TimerController _timerController;
        private GameResetOrEndManager _resetManager;
        private UnitStorage _unitStorage;
        public TankDestroyingController(UnitStorage unitStorage, TimerController timerController, GameResetOrEndManager gameResetManager)
        {
            _unitStorage = unitStorage;
            _resetManager = gameResetManager;
            _timerController = timerController;
            for(int i =0; i < _unitStorage.Gamers.Count; i++)
            {
                _unitStorage.Gamers[i].wasKilled += DestroyTank;
            }

            playerTankWasDestroed += _resetManager.PlayerLost;
            allEnemyTanksWasDestroed += _resetManager.PlayerWin;
        }

        private void DestroyTank(IGamer gamer)
        {
            gamer.GetParticleExplosion.Play();
            var timer = new TimerWhithParameters<IGamer>(new TimerData(0.3f, _timerController), gamer);
            timer.TimerIsOver += ShowWrackedObject;
        }

        private void ShowWrackedObject(IGamer gamer)
        {
            gamer.GetTankObject.SetActive(false);
            gamer.GetWrackObject.SetActive(true);

            if(gamer is Player)
            {
                playerTankWasDestroed.Invoke();
            } else
            {
                var enemies = _unitStorage.Enemies;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if(enemies[i].AliveStateController.State.IsAlive)
                    {
                        return;
                    }
                }
                allEnemyTanksWasDestroed.Invoke();
            }
        }
    }
}

