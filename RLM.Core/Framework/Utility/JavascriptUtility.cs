using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class JavascriptUtility
    {
        // Methods
        public static string EscapeString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            return text.Replace(@"\", @"\\").Replace("\"", "\\\"");
        }
    }


}
