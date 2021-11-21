using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class ControlLevelBridge
    {
        public INumberOfDeadEnemies NumberOfDeadEnemies;
        public ITakeDamagePlayer TakeDamagePlayer;

        public ControlLevelBridge(INumberOfDeadEnemies numberOfDeadEnemies, ITakeDamagePlayer takeDamagePlayer)
        {
            NumberOfDeadEnemies = numberOfDeadEnemies;
            TakeDamagePlayer = takeDamagePlayer;
        }
    }
}

