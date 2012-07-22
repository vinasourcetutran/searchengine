using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class EnumHelper
    {
        public static T GetEnumvalue<T>(string value)
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
