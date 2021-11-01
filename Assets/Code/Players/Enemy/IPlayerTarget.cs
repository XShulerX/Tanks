using System;
using UnityEngine;

namespace MVC
{
    public interface IPlayerTarget
    {
        event Action<Vector3> OnMouseUpChange;
    }
}