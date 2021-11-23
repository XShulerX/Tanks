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
        private GameObject _prefab;
        private Transform _container;

        public GameObject GetPrefab { get => _prefab; }
        public Transform GetContainer { get => _container; }

        public PoolModel()
        {
            _prefab = Resources.Load<GameObject>("Bullet");
            _container = GameObject.FindObjectOfType<GameStarter>().transform;
        }




    }
}
