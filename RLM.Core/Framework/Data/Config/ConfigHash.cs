using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RLM.Core.Framework.Data.Config
{
    public class ConfigHash : Hashtable, IConfigurable
    {
        public object GetKey(string key)
        {
            key = key.ToLower();
            if (this[key] == null) {
                throw new Exception(string.Format("The key '{0}' was not existed.",key));
            }
            return this[key];
        }

        public object GetKey(string key, object defaultValue)
        {
            key = key.ToLower();
            if (this[key] == null) { return defaultValue; }
            return this[key];
        }

        public void Add(string key, object value)
        {
            base[key.ToLower()] = value;
        }

        /// <summary>
        /// strValue in format: key1::value1;;key2::value2;;
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void FromString(string strValue)
        {
            if(string.IsNullOrWhiteSpace(strValue)){return;}
            string[] items = strValue.Split(new string[]{";;"},StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                string[] subItem = item.Split(new string[]{"::"}, StringSplitOptions.None);
                if (subItem.Length != 2) { continue; }
                this.Add(subItem[0], subItem[1]);
            }
        }
    }
}
