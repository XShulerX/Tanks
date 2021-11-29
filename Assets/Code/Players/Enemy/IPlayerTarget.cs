using System;
using UnityEngine;

namespace MVC
{
    public interface IPlayerTarget
    {
        event Action<IEnemy> OnMouseUpChange;
    }
}