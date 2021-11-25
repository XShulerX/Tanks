using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class LoadUnitStorage : ICommand
    {
        public bool Succeeded { get; private set; }
        private UnitStorage _unitStorage;

        public LoadUnitStorage(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public bool Load(GameMemento savedData)
        {
            foreach (var enemy in _unitStorage.Enemies)
            {
                enemy.TankElement = savedData.enemiesMementos[enemy.Id].element;
                enemy.CurrentHealthPoints = savedData.enemiesMementos[enemy.Id].curentHP;
                enemy.MaxHP = savedData.enemiesMementos[enemy.Id].maxHP;
                enemy.UpdateHelthView();
                enemy.UpdateTurretMaterialFromLoad();
            }

            _unitStorage.player.CurrentHealthPoints = savedData.playerMemento.hp;
            _unitStorage.player.TankElement = savedData.playerMemento.element;
            _unitStorage.player.UpdateHelthView();
            _unitStorage.player.UpdateTurretMaterialFromLoad();

            Succeeded = true;
            return Succeeded;
        }
    }
}

