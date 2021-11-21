using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public abstract class Pool
    {

        public List<GameObject> PoolOwner;
        public bool _autoExpand { get; set; } = true;
        private const int DEFAULT_COUNT_OF_AMMO = 15;
        public void CreatePool()
        {
            PoolOwner = new List<GameObject>();
            for (int i = 0; i < DEFAULT_COUNT_OF_AMMO; i++)
            {
                CreateObject();
            }
        }

        public virtual bool HasFreeElement(out GameObject element)
        {
            foreach (var poolElement in PoolOwner)
            {
                if (!poolElement.gameObject.activeInHierarchy)
                {
                    element = poolElement;
                    poolElement.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }


        public virtual GameObject GetFreeElement()
        {
            if (HasFreeElement(out var element))
            {
                return element;
            }

            if (_autoExpand)
            {
                return CreateObject(true);
            }

            return null;
        }

        public abstract GameObject CreateObject(bool isActiveByDefault = false, Transform transform = null);

    }
}

