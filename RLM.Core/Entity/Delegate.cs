using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace RLM.Core.Entity
{
    public delegate T XmlNodeToItem<T>(XPathNavigator node) where T : class, new();
    class Delegate
    {
    }
}
