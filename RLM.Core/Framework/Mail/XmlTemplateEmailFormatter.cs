using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;
using System.Web;

namespace RLM.Core.Framework.Mail
{
    public class XmlTemplateEmailFormatter : IEmailFormatter
    {
        // Methods
        public EmailTemplate Format(EmailTemplate emailTemplate, ParameterCollection parameterHash)
        {
            string from = this.FormatSection(emailTemplate.From, emailTemplate, parameterHash);
            string to = this.FormatSection(emailTemplate.To, emailTemplate, parameterHash);
            string cc = string.IsNullOrEmpty(emailTemplate.Cc) ? string.Empty : this.FormatSection(emailTemplate.Cc, emailTemplate, parameterHash);
            string bcc = string.IsNullOrEmpty(emailTemplate.Bcc) ? string.Empty : this.FormatSection(emailTemplate.Bcc, emailTemplate, parameterHash);
            string subject = "=?UTF-8?B?" + Convert.ToBase64String(Encoding.UTF8.GetBytes(this.FormatSection(emailTemplate.Subject, emailTemplate, parameterHash))) + "?=";
            string body = this.FormatSection(emailTemplate.Body, emailTemplate, parameterHash);
            EmailTemplate template = new EmailTemplate(subject, from, to, cc, bcc, body, emailTemplate.BodyContentType, emailTemplate.BodyEncoding, emailTemplate.LinkedResources);
            //if (Logger.IsDebugEnabled)
            //{
            //    Logger.Debug("Formatted email template: " + template, new object[0]);
            //}
            return template;
        }

        protected string FormatOneField(string sectionTemplate, EmailTemplate emailTemplate, string fieldName, string fieldValue, bool htmlEncode)
        {
            string oldValue = string.Format("${{{0}}}", fieldName);
            string newValue = (emailTemplate.BodyContentType.Equals("text/html") && htmlEncode) ? HttpUtility.HtmlEncode(fieldValue).Replace("\n", "<br />") : fieldValue;
            return sectionTemplate.Replace(oldValue, newValue);
        }

        protected string FormatSection(string sectionTemplate, EmailTemplate emailTemplate, ParameterCollection parameterHash)
        {
            string str = sectionTemplate;
            foreach (Parameter parameter in parameterHash)
            {
                string name = parameter.Name;
                object obj2 = parameter.Value;
                bool htmlEncode = parameter.HtmlEncode;
                if ((name != null) && (obj2 != null))
                {
                    str = this.FormatOneField(str, emailTemplate, name.ToString(), obj2.ToString(), htmlEncode);
                }
            }
            return str;
        }
    }


}
