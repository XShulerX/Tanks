using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class TankDestroyingController
    {

        public TankDestroyingController(List<IGamer> gamersList)
        {
            for(int i =0; i< gamersList.Count; i++)
            {
                gamersList[i].wasKilled += DestroyTank;
            }
        }

        private void DestroyTank(IGamer gamer)
        {
            gamer.GetParticleExplosion.Play();
            ShowWrackedObject(gamer);
        }

        private void ShowWrackedObject(IGamer gamer)
        {
            gamer.GetTankObject.SetActive(false);
            gamer.GetWrackObject.SetActive(true);
        }
    }
}

