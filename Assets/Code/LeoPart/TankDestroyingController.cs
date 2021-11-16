using System.Collections.Generic;

namespace MVC
{
    public class TankDestroyingController
    {
        private TimerController _timerController;
        public TankDestroyingController(List<IGamer> gamersList, TimerController timerController)
        {
            _timerController = timerController;
            for(int i =0; i< gamersList.Count; i++)
            {
                gamersList[i].wasKilled += DestroyTank;
            }
        }

        private void DestroyTank(IGamer gamer)
        {
            gamer.GetParticleExplosion.Play();
            var timer = new TimerWhithParameters<IGamer>(new TimerData(0.3f, _timerController), gamer);
            timer.TimerIsOver += ShowWrackedObject;

            //var timer = new TimerData(0.3f, gamer, _timerController);
            //timer.TimerEndWithGamer += ShowWrackedObject;

        }

        private void ShowWrackedObject(IGamer gamer)
        {
            gamer.GetTankObject.SetActive(false);
            gamer.GetWrackObject.SetActive(true);
        }
    }
}

