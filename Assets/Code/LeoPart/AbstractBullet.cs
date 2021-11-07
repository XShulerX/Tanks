using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public abstract class AbstractBullet : Pool
    {
        private string _typeBullet;

        public AbstractBullet(string type)
        {
            _typeBullet = type;
        }
    }
}

