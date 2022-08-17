using System;
using UnityEngine;

namespace Databases
{
    [Serializable]
    public class TowerVo
    {
        public int id;
        public int price;
        public float range;
        public float damage;
        public float attackCooldown;
        public GameObject towerPrefab;
    }
}