using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SearchEngine.Bot.Entity.Mail;

namespace SearchEngine.Configuration
{
    public class SmtpElement : ConfigurationElement, ISmtp
    {
        [ConfigurationProperty("serverName", DefaultValue = "localhost")]
        public string ServerName
        {
            get { return (string)this["serverName"]; }
        }
        
        [ConfigurationProperty("enableSSL")]
        public bool EnableSSL
        {
            get { return (bool)this["enableSSL"]; }
        }

        [ConfigurationProperty("port", DefaultValue = 25)]
        public int Port
        {
            get { return (int)this["port"]; }
        }

        [ConfigurationProperty("userName", DefaultValue = "")]
        public string UserName
        {
            get { return (string)this["userName"]; }
        }

        [ConfigurationProperty("password", DefaultValue = "")]
        public string Password
        {
            get { return (string)this["password"]; }
        }

        #region Default sender email
        [ConfigurationProperty("defaultSender", DefaultValue = "")]
        public string DefaultSender
        {
            get { return (string)this["defaultSender"]; }
        }

        [ConfigurationProperty("defaultCC", DefaultValue = "")]
        public string DefaultCC
        {
            get { return (string)this["defaultCC"]; }
        }

        [ConfigurationProperty("defaultBCC", DefaultValue = "")]
        public string DefaultBCC
        {
            get { return (string)this["defaultBCC"]; }
        }
        #endregion
    }
}
