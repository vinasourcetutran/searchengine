using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace RLM.Core.Framework.Cache
{
    public class CacheHelper
    {
        #region variable
        static CacheManager cacheManager = null;
        #endregion

        #region Constructor
        #endregion

        #region Properties
        public static CacheManager CacheManager
        {
            get
            {
                if (cacheManager == null)
                {
                    cacheManager = CacheFactory.GetCacheManager();
                }
                return cacheManager;
            }
        }

        public static void Add<T>(string key, T data)
        {
            AbsoluteTime absoulteTime = new AbsoluteTime(TimeSpan.FromMinutes(30));
            CacheManager.Add(key, data,
                CacheItemPriority.Normal, null, absoulteTime);
        }

        public static T Get<T>(string key)
        {
            return (T)CacheManager[key];
        }
        #endregion
    }
}
