using System.Data;
using System.IO;
using UnityEngine;

namespace MVC
{
    public sealed class SaveAndLoadRepository
    {
        private MementosSaver _mementoSaver;
        private LoadCommandManager _commandManager;

        private readonly IData<GameMemento> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveAndLoadRepository(MementosSaver mementosSaver, LoadCommandManager loadCommandManager)
        {
            _mementoSaver = mementosSaver;
            _commandManager = loadCommandManager;
            _data = new JsonData<GameMemento>();
            _path = Path.Combine(Application.dataPath, _folderName);
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

            var savedData= _data.Load(file);
            _commandManager.Load(savedData);
        }
    }
}