using System.Collections.Generic;
using Databases;

namespace Signals
{
    public class SignalWaveStart
    {
        public List<WaveInfoVo> WaveInfo;
        public float WaveDelay;
        
        public SignalWaveStart(List<WaveInfoVo> waveInfo, float waveDelay = 0)
        {
            WaveInfo = waveInfo;
            WaveDelay = 0;
        }
    }
}