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
            public int enemyID;
            public Vector3 Position;
            public Vector3 Rotation;
            public Enemy EnemyPrefab;
        }

        [SerializeField]
        private List<EnemyInfo> _enemies;

        public (Enemy[], int[], Vector3[], Vector3[]) GetEnemies()
        {
            var (enemies, enemiesID, enemyPosition, enemyRotation) =
                (new Enemy[_enemies.Count],
                new int[_enemies.Count],
                new Vector3[_enemies.Count],
                new Vector3[_enemies.Count]);
            for(var i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                if(enemy.EnemyPrefab == null)
                {
                    throw new InvalidOperationException("Enemy prefab not found");
                }
                enemies[i] = enemy.EnemyPrefab;
                enemiesID[i] = enemy.enemyID;
                enemyPosition[i] = enemy.Position;
                enemyRotation[i] = enemy.Rotation;
            }
            return (enemies, enemiesID, enemyPosition, enemyRotation);
        }
    }
}