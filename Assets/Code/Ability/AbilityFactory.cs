using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class AbilityFactory
    {
        private UnitStorage _unitStorage;
        private TimerController _timerControllerl;
        private Player _player;
        private GameObject _box;

        public AbilityFactory(TimerController timerControllerl, Player player, GameObject box, UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
            _timerControllerl = timerControllerl;
            _player = player;
            _box = box;
        }

        public T Create<T>(BulletPool pool, AbilityModel abilityModel) where T: Ability
        {
            Ability ability = abilityModel.Element switch
            {
                Elements.Fire => new FireAbility(pool, abilityModel).SetBox(_box).SetTimerController(_timerControllerl),
                Elements.Water => new WaterAbility(pool, abilityModel).SetPlayer(_player),
                Elements.Terra => new TerraAbility(pool, abilityModel).SetPlayer(_player).SetEnemies(_unitStorage),
                _ => throw new Exception ("Неизвестный элемент")
            };

            return (T)ability;
        }
    }
}