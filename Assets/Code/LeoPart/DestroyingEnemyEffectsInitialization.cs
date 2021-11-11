using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class DestroyingEnemyEffectsInitialization
    {
        private List<IGamer> _gamersList;
        private TimerController _timerController;

        private const float DELAY_BEFORE_DELETING = 2f;

        public DestroyingEnemyEffectsInitialization(List<IGamer> gamersList, TimerController timerController)
        {
            _gamersList = gamersList;
            _timerController = timerController;

            for(int i =0; i< _gamersList.Count; i++)
            {
                _gamersList[i].wasKilled += WrackEnemy;
            }
        }

        private void WrackEnemy(IGamer enemy)
        {
            var transform = enemy.GetGameObject.transform;
            CreateTimerExplosion(enemy, transform);
            CreateWrackedObject(enemy, transform);
        }
        private void CreateTimerExplosion(IGamer enemy, Transform transform)
        {
            var timer = new TimeData(DELAY_BEFORE_DELETING, CreateExplosion(transform, enemy));
            timer.OnTimerEndWithBool += DeleteGameObject;
            _timerController.AddTimer(timer);
        }

        private GameObject CreateExplosion(Transform transform, IGamer enemy)
        {
            var explosion = UnityEngine.Object.Instantiate(enemy.GetParticleExplosion.gameObject);
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            return explosion;
        }



        private void CreateWrackedObject(IGamer enemy, Transform transform)
        {
            enemy.GetGameObject.SetActive(false);
            var wrack = UnityEngine.Object.Instantiate(enemy.GetWrackObject);
            wrack.transform.position = transform.position;
        }



        private void DeleteGameObject(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

