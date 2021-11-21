using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class BulletPool: Pool
    {
        private GameObject _prefab;
        private Transform _container;


        public GameObject GetPrefab { get => _prefab; }
        public Transform GetContainer { get => _container; }

        public BulletPool(GameObject prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
            CreatePool();
        }

        public override GameObject CreateObject(bool isActiveByDefault = false, Transform transform = null)
        {
            var createObject = Object.Instantiate(_prefab, _container);
            createObject.gameObject.SetActive(isActiveByDefault);
            PoolOwner.Add(createObject);
            return createObject;
        }

    }
}

