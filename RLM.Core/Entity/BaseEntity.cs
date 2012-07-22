using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RLM.Core.Framework.Log;

namespace RLM.Core.Entity
{
    public class BaseEntity:Items
    {
        public BaseEntity()
        {
            if (string.IsNullOrEmpty(this.Values)) { return; }
            LoadCustomProperties(this.Values);
        }
        #region Custome fields Methods
        #endregion
        #region Methods
        public virtual void LoadCustomProperties(string xml) {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                LoadCustomProperties(xmlDoc);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public virtual void LoadCustomProperties(XmlDocument node) { }
        #endregion
    }
}
