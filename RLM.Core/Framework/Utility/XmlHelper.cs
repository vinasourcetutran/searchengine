using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RLM.Core.Entity;
using RLM.Core.Framework.Log;
using System.Reflection;

namespace RLM.Core.Framework.Utility
{
    using System.Collections;

    public class XmlHelper
    {
        #region Xml navigator
        public static IList<XmlNode> GetXmlNodesByXPath(XmlDocument xmlDoc, string xpath)
        {
            throw new Exception("This function will be implemented soon");
        }

        public static XmlNode GetXmlNodeByXPath(XmlDocument xmlDoc, string xpath,int index)
        {
            if (index < 0) { throw new Exception("Invalid index value in GetXmlNodeByXPath"); }
            IList<XmlNode> nodes = GetXmlNodesByXPath(xmlDoc, xpath);
            if (nodes == null || nodes.Count <= 0 || nodes.Count<=index) { return null; }
            return nodes[index];
        }

        public static XmlNode GetXmlNodeByXPath(XmlDocument xmlDoc, string xpath)
        {
            return GetXmlNodeByXPath(xmlDoc, xpath, 0);
        }

        public static string GetAttribute(XmlNode xmlNode, string attrName)
        {
            if (xmlNode.Attributes[attrName] == null) { return null; }
            XmlAttribute attr = xmlNode.Attributes[attrName];
            return attr.Value;
        }

        public static Pairs GetAttributes(XmlNode xmlNode,bool removeIfNull)
        {
            Pairs pairs = new Pairs();
            foreach (XmlAttribute attr in xmlNode.Attributes)
            {
                if (removeIfNull && string.IsNullOrEmpty(attr.Value)) { continue; }
                pairs[attr.Name] = attr.Value;
            }
            return pairs;
        }

        #endregion

        public static string EntityToXml(object entity)
        {
            try
            {
                if (entity == null) { return string.Empty; }
                StringBuilder sb = new StringBuilder();
                MemberInfo[] members = entity.GetType().GetMembers();
                sb.Append("<"+entity.GetType().Name+">");
                foreach (MemberInfo item in members)
                {
                    if (item.MemberType != MemberTypes.Property) { continue; }
                    object value = entity.GetType().InvokeMember(item.Name, BindingFlags.GetProperty, null, entity, null);
                    //if (value == null) { continue; }
                    sb.Append(string.Format(
                            "<{0}>{1}</{0}>",
                            item.Name,
                            value
                        ));
                }
                sb.Append("</" + entity.GetType().Name + ">");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        #region Hash
        public static RLM.Core.Entity.Hash<string> XmlToHash(string xml)
        {
            Hash<string> hash = new Hash<string>();
            try
            {
                if (string.IsNullOrEmpty(xml))
                {
                    return hash;
                }

                XmlDocument xmlDoc=new XmlDocument();
                xmlDoc.LoadXml(xml);
                foreach(XmlNode node in xmlDoc.ChildNodes[0].ChildNodes)
                {
                    hash[node.Name] = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return hash;
        }

        #endregion
    }
}
