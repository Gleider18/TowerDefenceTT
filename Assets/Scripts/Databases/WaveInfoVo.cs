using System;
using System.Collections.Generic;

namespace Databases
{
    [Serializable]
    public class WaveInfoVo
    {
        public List<EnemiesOnWaveVo> Enemies;
        public float WaveTime;
    }
}