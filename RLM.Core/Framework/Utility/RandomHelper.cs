using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class RandomHelper
    {
        public static int GetRandom(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to);
        }
    }
}
