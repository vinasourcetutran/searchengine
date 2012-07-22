using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class Reflection
    {
        public static Type GetType(string className)
        {
            return Type.GetType(className);
        }
    }
}
