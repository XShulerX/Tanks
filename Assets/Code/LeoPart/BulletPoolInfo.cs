using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MVC
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "GamePlay/New BulletPoolInfo")]
    class BulletPoolInfo : ScriptableObject
    {
        [SerializeField] private List<GameObject> _bulletPrefabs;


        public List<GameObject> GetBulletPrefabs { get => _bulletPrefabs; }




    }
}
