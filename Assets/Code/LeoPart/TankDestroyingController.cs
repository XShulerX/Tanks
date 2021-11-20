using System;

namespace MVC
{
    public class TankDestroyingController
    {
        public event Action tankWasDestroed = delegate () { };
        
        private TimerController _timerController;
        private GameResetManager _resetManager;
        public TankDestroyingController(UnitStorage unitStorage, TimerController timerController, GameResetManager gameResetManager)
        {
            _resetManager = gameResetManager;
            _timerController = timerController;
            for(int i =0; i< unitStorage.gamers.Count; i++)
            {
                unitStorage.gamers[i].wasKilled += DestroyTank;
            }

            tankWasDestroed += _resetManager.PlayerLost;
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
                tankWasDestroed.Invoke();
            }
        }
    }
}

