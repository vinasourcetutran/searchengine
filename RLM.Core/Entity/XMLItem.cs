using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
namespace RLM.Core.Entity
{
    public class XmlItem:XmlDocument
    {
        #region Constructor
        public XmlItem(string xml)
        {
            this.LoadXml(xml);
        }
        public XmlItem()
        {
        }
        #endregion
        #region Methods
        XPathNodeIterator Get(string xpath)
        {
            if (string.IsNullOrEmpty(xpath) || this.DocumentElement==null) { return null; }
            XPathNavigator nav = this.CreateNavigator();
            return nav.Select(xpath);
        }
        public IList<T> GetList<T>(string xpath, XmlNodeToItem<T> convertor) where T:class,new()
        {
            IList<T> ilist = new List<T>();
            XPathNodeIterator iter = Get(xpath);
            if (iter == null) { return ilist; }
            while (iter.MoveNext())
            {
                XPathNavigator nav = iter.Current.Clone();
                T item = convertor(nav);
                ilist.Add(item);
            }
            return ilist;
        }
        #endregion
    }
}
