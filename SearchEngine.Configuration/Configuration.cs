using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SearchEngine.Configuration
{
    public class Configuration
    {
        #region Variables
        static ConfigurationSection instance;
        #endregion

        #region Properties
        #endregion

        #region Static methods
        public static ConfigurationSection GetInstance()
        {
            try
            {
                if (instance == null)
                {
                    instance = (ConfigurationSection)ConfigurationManager.GetSection("searchEngineConfiguration");
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
