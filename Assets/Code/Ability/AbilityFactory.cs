using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class AbilityFactory
    {
        private TimerController _timerControllerl;
        private Player _player;
        private GameObject _box;
        private List<IEnemy> _enemies;

        public AbilityFactory(TimerController timerControllerl, Player player, GameObject box, List<IEnemy> enemies)
        {
            _timerControllerl = timerControllerl;
            _player = player;
            _box = box;
            _enemies = enemies;
        }

        public T Create<T>(BulletPool pool, AbilityModel abilityModel) where T: Ability
        {
            Ability ability = abilityModel.Element switch
            {
                Elements.Fire => new FireAbility(pool, abilityModel).SetBox(_box).SetTimerController(_timerControllerl),
                Elements.Water => new WaterAbility(pool, abilityModel).SetPlayer(_player),
                Elements.Terra => new TerraAbility(pool, abilityModel).SetPlayer(_player).SetEnemies(_enemies),
                _ => throw new Exception ("Неизвестный элемент")
            };

            return (T)ability;
        }
    }
}