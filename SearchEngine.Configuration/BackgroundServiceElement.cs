using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SearchEngine.Configuration
{
    public class BackgroundServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Number of paralle thread process data in background
        /// </summary>
        [ConfigurationProperty("maxThread", DefaultValue = "1")]
        public int MaxThread
        {
            get { return (int)this["maxThread"]; }
        }

        /// <summary>
        /// Number of time, main thread wait to keep thread alive. Unit in milisecond
        /// </summary>
        [ConfigurationProperty("mainThreadWaitingTime", DefaultValue = "30000")]
        public int MainThreadWaitingTime
        {
            get { return (int)this["mainThreadWaitingTime"]; }
        }
    }
}
