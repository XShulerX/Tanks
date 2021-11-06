using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MVC
{
    public class PoolModel
    {
        private List<GameObject> _prefabs;
        private Transform _container;

        public List<GameObject> GetPrefabs { get => _prefabs; }
        public Transform GetContainer { get => _container; }

        public PoolModel()
        {
            _container = GameObject.FindObjectOfType<GameStarter>().transform;
            _prefabs = Resources.Load<BulletPoolInfo>("BulletPoolInfo").GetBulletPrefabs;
        }




    }
}
