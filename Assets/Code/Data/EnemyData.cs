using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Data/EnemySettings")]
    public sealed class EnemyData : ScriptableObject
    {
        [Serializable]
        private struct EnemyInfo
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public Enemy EnemyPrefab;
        }

        [SerializeField]
        private List<EnemyInfo> _enemies;

        public (Enemy[], Vector3[]) GetEnemies()
        {
            var (enemies, enemyPosition) = (new Enemy[_enemies.Count], new Vector3[_enemies.Count]);
            for(var i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                enemies[i] = enemy.EnemyPrefab;
                enemyPosition[i] = enemy.Position;
            }
            return (enemies, enemyPosition);
        }
    }
}