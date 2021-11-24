using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public interface ICommand
    {
        bool Succeeded { get; }

        bool Load(GameMemento gameMemento);
    }
}

