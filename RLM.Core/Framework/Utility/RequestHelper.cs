using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RLM.Core.Framework.Utility
{
    public class RequestHelper
    {
        public static int GetInt32(string name, int defaultValue)
        {
            try
            {
                string value = HttpContext.Current.Request.Params[name];
                return !string.IsNullOrEmpty(value) ? int.Parse(value) : defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static string GetString(string name, string defaultValue)
        {
            try
            {
                string value = HttpContext.Current.Request.Params[name];
                return !string.IsNullOrEmpty(value) ? value : defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
