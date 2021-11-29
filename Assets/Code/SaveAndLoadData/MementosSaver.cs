using System.Collections.Generic;

namespace MVC
{
    public class MementosSaver
    {
        private UnitStorage _unitStorage;
        private GameResetOrEndManager _gameResetManager;
        private TurnController _turnController;
        private PlayerAbilityController _playerAbilityController;

        private List<GameMemento> _gameMementos = new List<GameMemento>(8);

        public MementosSaver(UnitStorage unitStorage, GameResetOrEndManager gameResetManager, TurnController turnController, PlayerAbilityController playerAbilityController)
        {
            _unitStorage = unitStorage;
            _gameResetManager = gameResetManager;
            _turnController = turnController;
            _playerAbilityController = playerAbilityController;

            _turnController.endGlobalTurn += SaveMementos;
            _gameResetManager.sceneResetState += SaveMementosAfterReset;

            SaveMementos();
        }

        private void SaveMementosAfterReset(bool isInReset)
        {
            if (!isInReset)
            {
                _gameMementos.RemoveAt(_gameMementos.Count - 1);
                SaveMementos();
            }
        }

        private void SaveMementos()
        {
            var enemiesMementos = new List<EnemyMementoData>(); 
            foreach (var enemy in _unitStorage.Enemies)
            {
                enemiesMementos.Add(new EnemyMementoData(enemy.Id, enemy.CurrentHealthPoints, enemy.MaxHP, enemy.GroundStateController.State.IsFly, enemy.TankElement));
            }
            var playerMemento = new PlayerMementoData(_unitStorage.player.CurrentHealthPoints, _unitStorage.player.TankElement);
            var turnMemento = new TurnMementoData(_turnController.GlobalTurnCount, _turnController.ShootedOrDeadEnemies);
            var stageMemento = new StageMementoData(_gameResetManager.AttemptsCount, _gameResetManager.UnitController.ForceModifer, _gameResetManager.StageCount);
            var abilitiesMemento = new List<AbilityMementoData>();
            foreach(var ability in _playerAbilityController.Abilities)
            {
                abilitiesMemento.Add(new AbilityMementoData(ability.Key, ability.Value.IsOnCooldown, ability.Value.CooldownTurns));
            }

            if (_gameMementos.Count > 7)
            {
                _gameMementos.RemoveAt(0);
            }
            
            _gameMementos.Add(new GameMemento(
                enemiesMementos,
                playerMemento,
                abilitiesMemento,
                turnMemento,
                stageMemento
                ));
            
        }

        public GameMemento GetLastMementoForSave()
        {
            return _gameMementos[_gameMementos.Count - 1];
        }
    }
}