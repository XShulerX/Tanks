using System;

namespace MVC
{
    public class TankDestroyingController
    {
        public event Action playerTankWasDestroed = delegate () { };
        public event Action allEnemyTanksWasDestroed = delegate () { };

        private TimerController _timerController;
        private GameResetManager _resetManager;
        private UnitStorage _unitStorage;
        public TankDestroyingController(UnitStorage unitStorage, TimerController timerController, GameResetManager gameResetManager)
        {
            _unitStorage = unitStorage;
            _resetManager = gameResetManager;
            _timerController = timerController;
            for(int i =0; i < _unitStorage.gamers.Count; i++)
            {
                _unitStorage.gamers[i].wasKilled += DestroyTank;
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
                var enemies = _unitStorage.enemies;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if(!enemies[i].IsDead)
                    {
                        return;
                    }
                }
                allEnemyTanksWasDestroed.Invoke();
            }
        }
    }
}

