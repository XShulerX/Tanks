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
                Debug.Log(enemy.Id);
                enemy.TankElement = savedData.enemiesMementos[enemy.Id].element;
                enemy.CurrentHealthPoints = savedData.enemiesMementos[enemy.Id].hp;
                enemy.UpdateHelthView();
            }

            _unitStorage.player.CurrentHealthPoints = savedData.playerMemento.hp;
            _unitStorage.player.TankElement = savedData.playerMemento.element;
            _unitStorage.player.UpdateHelthView();

            Succeeded = true;
            return Succeeded;
        }
    }
}

