using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RLM.Core.Entity;
using System.Web;
using RLM.Core.Framework.Log;
using System.IO;

namespace RLM.Core.Framework.Utility
{
    public class UrlHelper
    {
        #region Param
        // return  format of param on query string with pair of (name, vaue)
        public static string ParamFormat<T>(string name, T value)
        {
            return string.Format("{0}={1}",name,value);
        }

        /// <summary>
        /// return true if pattern is exist in url
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsParamExist(string pattern, string url)
        {
            return Regex.IsMatch(url, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        }

        /// <summary>
        /// add pair of param (name, value) into url
        /// add new if pair of param is not exist. otherwise replace old value
        /// </summary>
        public static string AddParam<T>(string name, T value, string pattern, string url)
        {
            try
            {
                string paramValue = UrlHelper.ParamFormat<T>(name, value);
                string paramPattern = UrlHelper.ParamFormat<string>(name,pattern);
                if (!UrlHelper.IsParamExist(paramPattern, url))
                {
                    return string.Format("{0}{1}{2}", url, (url.IndexOf("?") >= 0 ? "&" : "?"), paramValue);
                }

                return StringHelper.Replace(paramPattern, paramValue, url);
            }
            catch (Exception ex)
            {
                return string.Format("{0}{1}{2}", url, (url.IndexOf("?") >= 0 ? "&" : "?"), UrlHelper.ParamFormat<T>(name, value));
            }
        }
        #endregion

        #region Create Url
        //public static string GetUrl(string baseUrl, IEntity entity, UrlAction action)
        //{
        //    try
        //    {
        //        return string.Format(
        //            "{0}/{1}/{2}/{3}/{4}",
        //            baseUrl,
        //            entity.EntityType,
        //            action,
        //            entity.EntityId,
        //            entity.EntityName
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex);
        //        return baseUrl;
        //    }
        //}
        #endregion

        #region Url
        public static string ResolveUrl(string originalUrl)
        {
            SystemMode mode = GetSystemMode();
            if (mode == SystemMode.Web)
            {
                return ResolveWebUrl(originalUrl);
            }
            return ResolveConsoleUrl(originalUrl);
        }
        public static string ResolveConsoleUrl(string originalUrl)
        {
            if (originalUrl == null)
                return string.Empty;
            // *** Absolute path - just return    
            if (originalUrl.IndexOf(":\\") != -1)
                return originalUrl;
            // *** Fix up image path for ~ root app dir directory   
            if (originalUrl.StartsWith("\\"))
            {
                originalUrl = Path.Combine(GetRootPath(),originalUrl.Substring(1));
            }

            if (originalUrl.StartsWith("~/"))
            {
                originalUrl = Path.Combine(GetRootPath(), originalUrl.Substring(2));
            }
            originalUrl = UrlHelper.Format(GetSystemMode(),originalUrl);
            return originalUrl;
        }

        public static string GetRootPath()
        {
            SystemMode mode = GetSystemMode();
            if (mode == SystemMode.Web)
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string ResolveWebUrl(string originalUrl)
        {
            if (originalUrl == null)
                return null;
            // *** Absolute path - just return    
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;
            // *** Fix up image path for ~ root app dir directory   
            if (originalUrl.StartsWith("~"))
            {
                string newUrl = "";
                if (HttpContext.Current != null)
                {
                    newUrl = (GetRootPath() + originalUrl.Substring(1).Replace("//", "/")).Replace("//", "/");
                }
                else            // *** Not context: assume current directory is the base directory           
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");
                // *** Just to be sure fix up any double slashes       
                return ReplaceBackslash(newUrl);
            }
            return ReplaceBackslash(originalUrl);
        }

        public static string Mappath(string baseUrl, string url)
        {
            if (string.IsNullOrEmpty(url)) { return string.Empty; }
            SystemMode mode = GetSystemMode();
            switch (mode)
            {
                case SystemMode.Web:
                    return HttpContext.Current.Server.MapPath(url);
                    break;
                case SystemMode.BackgroundService:
                case SystemMode.Console:
                    url = url.Replace("/","\\");
                    if(StringHelper.IsMatch("^([a-z]:\\\\)", url)){
                        return url;
                    }
                    if (url.StartsWith("~\\"))
                    {
                        url = url.Replace("~\\", "");
                    }

                    if (url.StartsWith("\\"))
                    {
                        url = url.Substring(1);
                    }
                    if (!string.IsNullOrEmpty(baseUrl))
                    {
                        return Path.Combine(baseUrl, url);
                    }
                    string current = GetRootPath();
                    return Path.Combine(current, url);
                    break;
                case SystemMode.Unknow:
                default:
                    return url;
                    break;
            }
            return url;
        }

        public static string GetRelativeUrl(SystemMode mode, string baseUrl, string url)
        {
            if (!url.Contains(baseUrl)) { return url; }
            url = url.Replace(baseUrl,"/");
            url = UrlHelper.Format(mode, url);
            return url;
        }

        

        public static SystemMode GetSystemMode()
        {
            if (HttpContext.Current != null && HttpContext.Current.Server != null) { return SystemMode.Web; }
            return SystemMode.BackgroundService;
        }
        #endregion

        public static string ReplaceBackslash(string url)
        {
            if (string.IsNullOrEmpty(url)) { return url; }
            url= url.Replace('\\', '/');
            return url;

        }

        #region Format
        public static string Format(string url, UrlParams items)
        {
            try
            {
                if (items == null) { return url; }
                foreach (UrlParam item in items)
                {
                    if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Value)) { continue; }
                    url = url.Replace("$" + item.Name, item.Value);
                }
                return url;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "/";
            }
        }

        public static string Format(SystemMode mode, string url)
        {
            string[] items = url.Split('/','\\');
            string joinIndicator = "\\";
            if (mode == SystemMode.Web)
            {
                joinIndicator = "/";
            }
            return String.Join(joinIndicator, items);
        }
        #endregion

        
    }
}
