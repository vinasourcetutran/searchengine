using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using System.Configuration;

namespace RLM.Core.Web.URLRewrite
{
    [Serializable, XmlRoot("RewriterConfig")]
    public class RewriterConfiguration
    {
        // Fields
        private RewriterRuleCollection rules;

        // Methods
        public static RewriterConfiguration GetConfig()
        {
            if (HttpContext.Current.Cache["RewriterConfig"] == null)
            {
                HttpContext.Current.Cache.Insert("RewriterConfig", ConfigurationManager.GetSection("RewriterConfig"));
            }
            return (RewriterConfiguration)HttpContext.Current.Cache["RewriterConfig"];
        }

        // Properties
        public RewriterRuleCollection Rules
        {
            get
            {
                return this.rules;
            }
            set
            {
                this.rules = value;
            }
        }
    }

 

}
