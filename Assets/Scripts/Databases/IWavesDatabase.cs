using System.Collections.Generic;

namespace Databases
{
    public interface IWavesDatabase
    {
       List<WaveInfoVo> GetWaves();
    }
}