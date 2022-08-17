using System;
using System.Collections.Generic;
using DefaultNamespace;
using Towers;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(fileName = "TowersDatabase", menuName = "Databases/TowersDatabase")]
    public class TowersDatabase : ScriptableObject, ITowersDatabase
    {
        [SerializeField] private List<TowerVo> towers;
        [SerializeField] private TowerPlaceController towerPlace;
        
        public TowerVo GetTowerById(int id)
        {
            foreach (var tower in towers)
            {
                if (tower.id == id)
                    return tower;
            }

            throw new Exception("There isn't tower with id: " + id);
        }

        public TowerPlaceController GetTowerPlace()
        {
            return towerPlace;
        }
    }
}