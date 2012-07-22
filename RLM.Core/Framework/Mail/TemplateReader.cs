using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;
using System.IO;
using System.Xml;
using System.Web;
using RLM.Core.Framework.Utility;
using NVelocityTemplateEngine.Interfaces;
using NVelocityTemplateEngine;


namespace RLM.Core.Framework.Mail
{
    public class TemplateReader
    {
        public static string GetContent(string templatePath, Dictionary<string, string> parameters)
        {
            try
            {
                StreamReader stream = new StreamReader(templatePath);
                string result = stream.ReadToEnd();
                stream.Close();
                foreach (string keyword in parameters.Keys)
                {

                    string value = parameters[keyword];
                    string keyword2 = string.Concat('{', keyword, '}');
                    result = result.Replace(keyword2, value);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static string GetXmlContent(string templatePath, Dictionary<string, string> parameters)
        {
            return GetXmlContent("email","{{0}}", templatePath, parameters);
        }
        public static string GetXmlContent(string rootTagName, string bookmarkFormat, string templatePath, Dictionary<string, string> parameters)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(templatePath);

                XmlNodeList resources = doc.SelectNodes(string.Format("/{0}/resources/resource",rootTagName));
                if (resources != null && resources.Count <= 0)
                {
                    return GetContent(templatePath, parameters);
                }

                XmlNode content = doc.SelectSingleNode(string.Format("/{0}/content",rootTagName));
                if (content == null) { return string.Empty; }
                string resourceContent = string.Empty;
                string mailContent = content.InnerText;
                foreach (XmlNode item in resources)
                {
                    Dictionary<string, string> childParam = new Dictionary<string, string>();
                    foreach (string key in parameters.Keys)
                    {
                        childParam.Add("parent."+key, parameters[key]);
                    }
                    try
                    {
                        XmlAttribute bookmark = item.Attributes["bookmark"];
                        if (bookmark != null)
                        {
                            if (item.Attributes["lazy"] != null && item.Attributes["lazy"].Value.ToLower()=="true")
                            {
                                parameters[bookmark.Value.Replace("{","").Replace("}","")] = GetResourceContent(item, childParam);
                            }
                            else
                            {
                                mailContent = mailContent.Replace(bookmark.Value, GetResourceContent(item, childParam));
                            }
                        }
                        else
                        {
                            resourceContent += GetResourceContent(item);
                        }
                    }
                    catch (Exception e) { Logger.Error(e); }
                }

                resourceContent = string.Format("{0}{1}", resourceContent, mailContent);
                foreach (string keyword in parameters.Keys)
                {

                    string value = parameters[keyword];
                    string keyword2 = StringHelper.Format(bookmarkFormat, keyword); // string.Concat('{', keyword, '}');
                    resourceContent = resourceContent.Replace(keyword2, value);
                }
                return resourceContent;
            }
            catch (Exception ex)
            {
                Logger.Error("{0},{1},{2},{3}", rootTagName, bookmarkFormat, templatePath,ex);
                return GetContent(templatePath, parameters);
            }
        }

        private static string GetResourceContent(XmlNode item, Dictionary<string, string> parameters)
        {
            try
            {
                string template = item.Attributes["template"].Value;
                string fileUrl = item.Attributes["fileUrl"].Value;
                fileUrl = UrlHelper.Mappath("~/", fileUrl);// System.IO.Path.GetFullPath(fileUrl);// HttpContext.Current.Server.MapPath(fileUrl)

                return GetXmlContent(fileUrl, parameters);
                //StreamReader stream = new StreamReader(fileUrl);
                //string result = stream.ReadToEnd();
                //stream.Close();

                //return string.Format(template, result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        private static string GetResourceContent(XmlNode item)
        {
            try
            {
                string template = item.Attributes["template"].Value;
                string fileUrl = item.Attributes["fileUrl"].Value;
                fileUrl = UrlHelper.Mappath("~/", fileUrl);// System.IO.Path.GetFullPath(fileUrl);// HttpContext.Current.Server.MapPath(fileUrl)

                StreamReader stream = new StreamReader(fileUrl);
                string result = stream.ReadToEnd();
                stream.Close();

                return string.Format(template, result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        public static EmailTemplateContent GetEmailTemplateContent(string templatePath, Dictionary<string, string> parameters)
        {
            EmailTemplateContent templateContent = new EmailTemplateContent();

            try
            {
                
                XmlDocument doc = new XmlDocument();
                doc.Load(templatePath);

                XmlNodeList resources = doc.SelectNodes("/email/resources/resource");
                if (resources != null && resources.Count <= 0)
                {
                    templateContent.Content = GetContent(templatePath, parameters);
                    return templateContent;
                }

                XmlNode item = doc.SelectSingleNode("/email/cc");
                if (item != null)
                {
                    templateContent.CC = item.InnerText;
                }

                item = doc.SelectSingleNode("/email/bcc");
                if (item != null)
                {
                    templateContent.Bcc = item.InnerText;
                }

                item = doc.SelectSingleNode("/email/subject");
                if (item != null)
                {
                    templateContent.Subject = item.InnerText;
                }

                item = doc.SelectSingleNode("/email/from");
                if (item != null)
                {
                    templateContent.From = item.InnerText;
                }

                item = doc.SelectSingleNode("/email/to");
                if (item != null)
                {
                    templateContent.To = item.InnerText;
                }


                XmlNode content = doc.SelectSingleNode("/email/content");
                if (content == null)
                {
                    templateContent.Content = string.Empty;
                    return templateContent;
                }
                string resourceContent = string.Empty;
                string mailContent = content.InnerText;
                foreach (XmlNode resource in resources)
                {
                    try
                    {
                        XmlAttribute bookmark = resource.Attributes["bookmark"];
                        if (bookmark != null)
                        {
                            mailContent = mailContent.Replace(bookmark.Value, GetResourceContent(resource));
                        }
                        else
                        {
                            resourceContent += GetResourceContent(resource);
                        }
                    }
                    catch (Exception e) { Logger.Error(e); }
                }

                resourceContent = string.Format("<div>{0}<br/>{1}</div>", resourceContent, mailContent);
                foreach (string keyword in parameters.Keys)
                {

                    string value = parameters[keyword];
                    string keyword2 = string.Concat('{', keyword, '}');
                    resourceContent = StringHelper.Replace(keyword2, value, resourceContent);
                }
                templateContent.Content = resourceContent;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                templateContent.Content = GetContent(templatePath, parameters);
            }
            return templateContent;
        }

        #region Velocity engine
        public static string GetVelocityTemplateContent(string templateFilePath, Core.Entity.Hash<object> parameters)
        {
            INVelocityEngine fileEngine =
                NVelocityEngineFactory.CreateNVelocityFileEngine(Path.GetDirectoryName(templateFilePath), true);

            return fileEngine.Process(parameters, Path.GetFileName(templateFilePath));
        }
        #endregion
    }
}

