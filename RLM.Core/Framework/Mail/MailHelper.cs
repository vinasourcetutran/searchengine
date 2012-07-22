using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;
using System.Net.Mail;
using System.Net;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Mail
{
    public class MailHelper
    {
        public static void Send(string to, string subject, string templateParth, Dictionary<string, string> parameters, ISmtp smtp)
        {
            try
            {
                Send(smtp.DefaultSender, to, subject, templateParth, parameters, smtp);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static void Send(string from,string to, string subject, string templateParth, Dictionary<string, string> parameters, ISmtp smtp)
        {
            try
            {
                Logger.InfoWithParam("From:{0}, To:{1},Subject:{2}", from, to, subject);
                Mailler mail = new Mailler(from, to, subject, templateParth, parameters);
                string[] emails;
                if (!string.IsNullOrEmpty(smtp.DefaultCC))
                {
                    emails = smtp.DefaultCC.Split(';');
                    foreach (string emailItem in emails)
                    {
                        try
                        {
                            mail.CC.Add(emailItem.Trim());
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(smtp.DefaultBCC))
                {
                    emails = smtp.DefaultBCC.Split(';');
                    foreach (string emailItem in emails)
                    {
                        try
                        {
                            mail.Bcc.Add(emailItem.Trim());
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }
                MailHelper.Send(mail, smtp);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static void Send(Mailler mail, ISmtp smtp)
        {
            SmtpClient client = new SmtpClient(smtp.ServerName, smtp.Port);
            if (smtp.UserName != string.Empty
                && smtp.Password != string.Empty)
            {
                client.EnableSsl = smtp.EnableSSL;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtp.UserName, smtp.Password);
            }

            client.Send(mail);
        }
    }
}
