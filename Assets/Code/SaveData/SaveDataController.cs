using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class SaveDataController
    {
        private InputController _inputController;
        private SaveAndLoadGameDataManager _manager;



        public SaveDataController(InputController inputController, MementosSaver mementosSaver, UnitStorage unitStorage, GameResetOrEndManager gameResetOrEndManager, TurnController turnController, PlayerAbilityController playerAbilityController)
        {
            _inputController = inputController;
            _manager = new SaveAndLoadGameDataManager(mementosSaver, unitStorage, gameResetOrEndManager, turnController, playerAbilityController);

            SignOnEvents();
        }

        private void SignOnEvents()
        {
            _inputController.SaveKeyIsPressed += SaveButtonPress;
            _inputController.LoadKeyIsPressed += LoadButtonPress;
        }

        protected void SaveButtonPress()
        {
            _manager.Save();
        }

        protected void LoadButtonPress()
        {
            _manager.Load();
        }
    }
}