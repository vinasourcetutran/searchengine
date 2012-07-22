using System.Collections;
using RLM.Core.Framework.Utility;
using System;
namespace RLM.Core.Entity
{
    [Serializable()]
    public class Hash<T>:Hashtable
    {
        public string Root { get; set; }
        public Hash():base()
        {
            Root = "root";
        }

        public Hash(string root)
            : base()
        {
            Root = root;
        }

        public T this[string key]
        {
            get
            {
                if (base[key.ToLower()] == null)
                {
                    return default(T);
                }
                return (T)base[key.ToLower()];
            }
            set
            {
                base[key.ToLower()] = value;
            }
        }

        public string GetDecodeString(string key)
        {
            T item = this[key];
            return HttpUtility.HtmlDecode(item.ToString());
        }

        public override string ToString()
        {
            try
            {
                string data = string.Empty;
                foreach (string key in this.Keys)
                {
                    data += string.Format("<{0}><![CDATA[{1}]]></{0}>", key, base[key]);
                }
                return string.Format("<{0}>{1}</{0}>", Root, data);
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public override object Clone()
        {
            Hash<T> hash = new Hash<T>();
            foreach(string key in this.Keys){
                hash[key] = this[key];
            }
            return hash;
        }
    }
}
