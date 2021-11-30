using System.Collections.Generic;
using UnityEngine; 

namespace MVC
{
    internal sealed class EnemyFireController : IExecute
    {
        private UnitStorage _unitStorage;

        public EnemyFireController(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;
        }

        public void Execute(float deltaTime)
        {
            foreach(var enemy in _unitStorage.Enemies)
            {
                if (enemy.IsYourTurn && enemy.AliveStateController.State.IsAlive && enemy.GroundStateController.State.IsOnGround)
                {
                    enemy.Fire(_unitStorage.Players[Random.Range(0,_unitStorage.Players.Count)].transform);
                    enemy.IsShoted = true;
                    enemy.IsYourTurn = false;
                }
            }
        }
    }
}