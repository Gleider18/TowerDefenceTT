using System.Collections.Generic;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(fileName = "WavesDatabase", menuName = "Databases/WavesDatabase")]
    public class WavesDatabase : ScriptableObject, IWavesDatabase
    {
        [SerializeField] private List<WaveInfoVo> _waveInfoVos;
        
        public List<WaveInfoVo> GetWaves()
        {
            return _waveInfoVos;
        }
    }
}