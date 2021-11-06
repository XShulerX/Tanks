using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MVC
{
    class BulletPoolsInitialization
    {

        private List<BulletPool> _bullets;
        private PoolModel _poolModel;

        public List<BulletPool> GetBullets { get => _bullets; }
        public BulletPoolsInitialization(PoolModel poolModel)
        {
            _poolModel = poolModel;
            _bullets = new List<BulletPool>();

            for (int i = 0; i < _poolModel.GetPrefabs.Count; i++)
            {
                _bullets.Add(new BulletPool(_poolModel.GetPrefabs[i], _poolModel.GetContainer));
            }
        }


    }
}
