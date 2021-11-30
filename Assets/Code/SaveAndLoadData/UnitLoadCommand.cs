using System.Collections.Generic;

namespace MVC
{
    public class UnitLoadCommand: ILoadCommand
    {
        public bool Succeeded { get; private set; }
        private UnitStorage _unitStorage;

        public UnitLoadCommand(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public bool Load(IMementoData mementoData)
        {
            if(mementoData is EnemyMementoData enemyMementoData)
            {
                foreach (var enemy in _unitStorage.Enemies)
                {
                    if (enemyMementoData.id == enemy.Id)
                    {
                        enemy.Load(mementoData);
                    }
                }
            }

            if (mementoData is PlayerMementoData playerMementoData)
            {
                for (int i = 0; i < _unitStorage.Players.Count; i++)
                {
                    (_unitStorage.Players[i] as ILoadeble).Load(playerMementoData);
                }
            }



                Succeeded = true;
            return Succeeded;
        }
    }
}