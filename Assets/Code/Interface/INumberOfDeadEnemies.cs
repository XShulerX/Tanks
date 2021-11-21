using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MVC
{
    public interface INumberOfDeadEnemies
    {
        public event Action OnLevelEnd;
        public int GetDeadEnemies { get; }
        LinkedList<IGamer> QueueGamers { get; }
        public void SetDefault(List<IGamer> gamersList);
    }
}

