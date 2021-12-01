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
                    var alivePlayers = new List<Player>();
                    foreach (var player in _unitStorage.Players)
                    {
                        if(player.AliveStateController.State.IsAlive)
                        {
                            alivePlayers.Add(player);
                        }
                    }
                    enemy.Fire(alivePlayers[Random.Range(0, alivePlayers.Count)].transform);
                    enemy.IsShoted = true;
                    enemy.IsYourTurn = false;
                }
            }
        }
    }
}