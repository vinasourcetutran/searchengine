using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Net.Mail;

namespace RLM.Core.Framework.Mail
{
    public class XmlEmailTemplate : EmailTemplate
    {
        // Fields
        private string xmlTemplateFile;

        // Methods
        public XmlEmailTemplate(string xmlTemplateFile)
        {
            this.xmlTemplateFile = xmlTemplateFile;
            XmlDocument xmlTemplate = new XmlDocument();
            xmlTemplate.Load(xmlTemplateFile);
            this.Init(xmlTemplate);
        }

        public XmlEmailTemplate(XmlDocument xmlTemplate)
        {
            this.Init(xmlTemplate);
        }

        private void Init(XmlDocument xmlTemplate)
        {
            XmlElement documentElement = xmlTemplate.DocumentElement;
            base.subject = xmlTemplate.SelectSingleNode("/email-template/subject").InnerText.Trim();
            base.from = xmlTemplate.SelectSingleNode("/email-template/addresses/entry[@name='From']").Attributes["value"].InnerText;
            base.to = xmlTemplate.SelectSingleNode("/email-template/addresses/entry[@name='To']").Attributes["value"].InnerText;
            XmlNode node = xmlTemplate.SelectSingleNode("/email-template/addresses/entry[@name='Cc']");
            if (node != null)
            {
                base.cc = node.Attributes["value"].InnerText;
            }
            XmlNode node2 = xmlTemplate.SelectSingleNode("/email-template/addresses/entry[@name='Bcc']");
            if (node2 != null)
            {
                base.bcc = node2.Attributes["value"].InnerText;
            }
            base.body = xmlTemplate.SelectSingleNode("/email-template/body").InnerText.Trim();
            base.bodyContentType = xmlTemplate.SelectSingleNode("/email-template/body/@format").InnerText;
            string innerText = xmlTemplate.SelectSingleNode("/email-template/body/@encoding").InnerText;
            try
            {
                base.bodyEncoding = Encoding.GetEncoding(innerText);
            }
            catch
            {
                base.bodyEncoding = Encoding.GetEncoding("iso-8859-1");
            }
            XmlNodeList list = xmlTemplate.SelectNodes("/email-template/embeddedResources/resource");
            if ((list != null) && (list.Count > 0))
            {
                string directoryName = Path.GetDirectoryName(this.xmlTemplateFile);
                foreach (XmlNode node3 in list)
                {
                    try
                    {
                        string str3 = node3.Attributes["name"].Value;
                        string str4 = node3.Attributes["path"].Value;
                        string str5 = "application/octet-stream";
                        if (node3.Attributes["content-type"] != null)
                        {
                            str5 = node3.Attributes["content-type"].Value;
                        }
                        if (!(string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(str4)))
                        {
                            LinkedResource item = new LinkedResource(Path.Combine(directoryName, str4));
                            item.ContentId = str3;
                            item.ContentType.Name = str3;
                            item.ContentType.MediaType = str5;
                            base.LinkedResources.Add(item);
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidEmailTemplateException("<embeddedResources> node is not in correct format.\r\n                            Example:\r\n                                <embeddedResources>\r\n                                    <resource name=\"logo\" path=\"images\\logo.gif\" content-type=\"image/jpeg\"/>\r\n                                </embeddedResources>", exception);
                    }
                }
            }
        }
    }


}
