using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var timer = new TimeData(0.3f, gamer, _timerController);
            timer.TimerEndWithGamer += ShowWrackedObject;

        }

        private void ShowWrackedObject(IGamer gamer)
        {
            if(gamer.GetTankObject != null)
            {
                gamer.GetTankObject.SetActive(false);
                gamer.GetWrackObject.SetActive(true);
            }
            
        }
    }
}

