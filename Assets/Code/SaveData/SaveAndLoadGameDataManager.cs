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

        private LoadHandler _loadHandler;

        public SaveAndLoadGameDataManager(MementosSaver mementosSaver, UnitStorage unitStorage, GameResetOrEndManager gameResetOrEndManager, TurnController turnController, PlayerAbilityController playerAbilityController)
        {
            _mementoSaver = mementosSaver;
            _data = new JsonData<GameMemento>();
            _path = Path.Combine(Application.dataPath, _folderName);

            _loadHandler = new LoadHandler(gameResetOrEndManager, playerAbilityController, turnController, unitStorage);
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

            _loadHandler.Load(savedData);




            // вот тут надо обновить данные для всех участников загрузки
            // можно прям тут, можно передать его кому то, для загрузки.
            // если прямо тут, то тебе в этот класс надо будет передать: 
            //UnitStorage, GameResetOrEndManager, TurnController, PlayerAbilityController для их обновления
        }
    }
}