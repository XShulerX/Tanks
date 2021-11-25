using System.Collections.Generic;

namespace MVC
{
    public class UnitLoadCommand
    {
        public bool Succeeded { get; private set; }
        private UnitStorage _unitStorage;

        public UnitLoadCommand(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public bool Load(PlayerMementoData playerMementoData, List<EnemyMementoData> enemyMementoDatas)
        {
            foreach (var enemy in _unitStorage.Enemies)
            {
                for (int i = 0; i < enemyMementoDatas.Count; i++)
                {
                    if (enemy.Id == enemyMementoDatas[i].id)
                    {
                        enemy.Load<EnemyMementoData>(enemyMementoDatas[i]);
                    }
                }
            }

            (_unitStorage.player as ILoadeble).Load<PlayerMementoData>(playerMementoData);
            Succeeded = true;
            return Succeeded;
        }
    }
}