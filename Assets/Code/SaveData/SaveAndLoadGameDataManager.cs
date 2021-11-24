using System.Data;
using System.IO;
using UnityEngine;

namespace MVC
{
    public sealed class SaveAndLoadGameDataManager
    {
        private MementosSaver _mementoSaver;

        private readonly IData<GameMemento> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        private UnitStorage _unitStorage;
        private GameResetOrEndManager _gameResetOrEndManager;
        private TurnController _turnController;
        private PlayerAbilityController _playerAbilityController;

        public SaveAndLoadGameDataManager(MementosSaver mementosSaver, UnitStorage unitStorage, GameResetOrEndManager gameResetOrEndManager, TurnController turnController, PlayerAbilityController playerAbilityController)
        {
            _mementoSaver = mementosSaver;
            _data = new JsonData<GameMemento>();
            _path = Path.Combine(Application.dataPath, _folderName);

            _unitStorage = unitStorage;
            _gameResetOrEndManager = gameResetOrEndManager;
            _turnController = turnController;
            _playerAbilityController = playerAbilityController;
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var saveGame = _mementoSaver.GetLastMementoForSave();

            _data.Save(saveGame, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }

            var savedData = _data.Load(file); // это готовый GameMemento

            _gameResetOrEndManager.ResetScene();
            LoadFromJson(savedData);




            // вот тут надо обновить данные для всех участников загрузки
            // можно прям тут, можно передать его кому то, для загрузки.
            // если прямо тут, то тебе в этот класс надо будет передать: 
            //UnitStorage, GameResetOrEndManager, TurnController, PlayerAbilityController для их обновления
        }

        private void LoadFromJson(GameMemento savedData)
        {
            
            foreach (var enemy in _unitStorage.Enemies)
            {
                Debug.Log(enemy.Id);
                enemy.TankElement = savedData.enemiesMementos[enemy.Id].element;
                enemy.CurrentHealthPoints = savedData.enemiesMementos[enemy.Id].hp;
            }

            _unitStorage.player.CurrentHealthPoints = savedData.playerMemento.hp;
            _unitStorage.player.TankElement = savedData.playerMemento.element;

            _gameResetOrEndManager.AttemptsCount = savedData.attemptsCount;
            _turnController.GlobalTurnCount = savedData.turnCount;
            _gameResetOrEndManager.UnitController.ForceModifer = savedData.forceModifier;
            _gameResetOrEndManager.StageCount = savedData.stageCount;

            for (int i = 0; i < _playerAbilityController.Abilities.Count; i++)
            {
                _playerAbilityController.Abilities[i].IsOnCooldown = savedData.abilitiesMemento[i].isOnCooldown;
                _playerAbilityController.Abilities[i].CooldownTurns = savedData.abilitiesMemento[i].cooldownTurns;
            }
        }
    }
}