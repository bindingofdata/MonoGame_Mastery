using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class RandomExt
    {
        public static float NextFloat(this Random rand, float max)
        {
            return (float)rand.NextDouble() * max;
        }

        public static float NextFloat(this Random rand, float max, float min)
        {
            return (float)(rand.NextDouble() * (max - min)) + min;
        }
    }
}
