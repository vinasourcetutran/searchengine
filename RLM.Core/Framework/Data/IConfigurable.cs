using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Data
{
    public interface IConfigurable
    {
        void FromString(string param);
        object GetKey(string key);
        object GetKey(string key, object defaultValue);
        void Add(string key, object value);
        string ToString();
    }
}
