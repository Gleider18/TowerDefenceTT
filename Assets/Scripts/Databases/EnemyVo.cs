using System;
using UnityEngine;

namespace Databases
{
    [Serializable]
    public class EnemyVo
    {
        public int id;
        public float speed;
        public float health;
        public int moneyForDefeat;
        public GameObject enemyPrefab;
    }
}