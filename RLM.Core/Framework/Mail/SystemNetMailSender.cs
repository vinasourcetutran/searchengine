using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using RLM.Core.Framework.Log;

namespace RLM.Core.Framework.Mail
{
    public class SystemNetMailSender : IEmailSender
    {
        // Fields
        private SmtpClient smtpClient;

        // Methods
        public SystemNetMailSender(string host, int port)
        {
            this.smtpClient = new SmtpClient(host, port);
        }

        public SystemNetMailSender(string host, int port, string username, string password)
        {
            this.smtpClient = new SmtpClient(host, port);
            this.smtpClient.Credentials = new NetworkCredential(username, password);
        }
        public SystemNetMailSender(SmtpConfig smtpConfig)
        {
            this.smtpClient = new SmtpClient(smtpConfig.Host, smtpConfig.Port);
            if (smtpConfig.UserName.Length <= 0) { return; }
            this.smtpClient.Credentials = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password);
        }

        protected MailMessage CreateMailMessage(EmailTemplate template)
        {
            MailMessage message = new MailMessage(template.From, template.To, template.Subject, template.Body);
            if (!string.IsNullOrEmpty(template.Cc))
            {
                message.CC.Add(template.Cc);
            }
            if (!string.IsNullOrEmpty(template.Bcc))
            {
                message.Bcc.Add(template.Bcc);
            }
            message.IsBodyHtml = !string.IsNullOrEmpty(template.BodyContentType) && template.BodyContentType.Equals("text/html");
            if ((template.LinkedResources != null) && (template.LinkedResources.Count > 0))
            {
                AlternateView item = AlternateView.CreateAlternateViewFromString(template.Body, template.BodyEncoding, "text/html");
                foreach (LinkedResource resource in template.LinkedResources)
                {
                    item.LinkedResources.Add(resource);
                }
                message.AlternateViews.Add(item);
            }
            return message;
        }

        public void Send(EmailTemplate template, IEmailFormatter formatter, ParameterCollection parameterHash)
        {
            try
            {
                EmailTemplate template2 = formatter.Format(template, parameterHash);
                MailMessage message = this.CreateMailMessage(template2);
                this.smtpClient.Send(message);
            }
            catch (Exception exception)
            {
                //Logger.Error("Error on sending mail: " + exception, new object[0]);
            }
        }

        // Properties
        public SmtpClient SmtpClient
        {
            get
            {
                return this.smtpClient;
            }
            set
            {
                this.smtpClient = value;
            }
        }
    }
}
