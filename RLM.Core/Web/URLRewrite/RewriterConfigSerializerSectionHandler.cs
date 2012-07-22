using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;

namespace RLM.Core.Web.URLRewrite
{
    public class RewriterConfigSerializerSectionHandler : IConfigurationSectionHandler
    {
        // Methods
        public object Create(object parent, object configContext, XmlNode section)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RewriterConfiguration));
            return serializer.Deserialize(new XmlNodeReader(section));
        }
    }


}
