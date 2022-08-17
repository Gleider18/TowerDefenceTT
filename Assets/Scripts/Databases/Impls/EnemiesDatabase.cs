using System;
using System.Collections.Generic;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(fileName = "EnemiesDatabase", menuName = "Databases/EnemiesDatabase")]
    public class EnemiesDatabase : ScriptableObject, IEnemiesDatabase
    {
        [SerializeField] private List<EnemyVo> enemies;

        public EnemyVo GetEnemyById(int id)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.id == id)
                    return enemy;
            }

            throw new Exception("There isn't enemy with id: " + id);
        }
    }
}