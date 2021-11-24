using System;
using UnityEngine;

namespace MVC
{
    public class AbilityFactory
    {
        private UnitStorage _unitStorage;
        private TimerController _timerControllerl;
        private GameObject _box;

        public AbilityFactory(TimerController timerControllerl, GameObject box, UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
            _timerControllerl = timerControllerl;
            _box = box;
        }

        public T Create<T>(BulletPool pool, AbilityModel abilityModel) where T: Ability
        {
            Ability ability = abilityModel.Element switch
            {
                Elements.Fire => new FireAbility(pool, abilityModel).SetBox(_box).SetTimerController(_timerControllerl),
                Elements.Water => new WaterAbility(pool, abilityModel).SetPlayer(_unitStorage.player),
                Elements.Terra => new TerraAbility(pool, abilityModel).SetPlayer(_unitStorage.player).SetEnemies(_unitStorage),
                _ => throw new Exception ("Неизвестный элемент")
            };

            return (T)ability;
        }
    }
}