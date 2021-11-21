using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MVC
{
    public class BulletPoolsInitialization
    {

        private BulletPool _bullets;
        private PoolModel _poolModel;

        public BulletPool GetBullets { get => _bullets; }
        public BulletPoolsInitialization(PoolModel poolModel)
        {
            _poolModel = poolModel;

            _bullets = new BulletPool(_poolModel.GetPrefab, _poolModel.GetContainer);
        }


    }
}
