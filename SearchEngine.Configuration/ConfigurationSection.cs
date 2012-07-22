using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SearchEngine.Configuration
{
    public class ConfigurationSection : System.Configuration.ConfigurationSection
    {

        [ConfigurationProperty("backgroundservice")]
        public BackgroundServiceElement BackgroundService
        {
            get { return (BackgroundServiceElement)this["backgroundservice"]; }
        }

        [ConfigurationProperty("smtp")]
        public SmtpElement Smtp
        {
            get { return (SmtpElement)this["smtp"]; }
        }

        [ConfigurationProperty("application")]
        public ApplicationElement Application
        {
            get { return (ApplicationElement)this["application"]; }
        }
    }
}
