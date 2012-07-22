using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using RLM.Core.Framework.Log;
using RLM.Core.Entity;
using System.Net;

namespace RLM.Core.Framework.Mail
{
   public class Mailler : MailMessage
    {
       public string TemplatePath { get; set; }
       public Dictionary<string,string> Parameters { get; set; }
       public Mailler(string from, string to, string subject, string templateParth, Dictionary<string, string> parameters)
       {
           try
           {
               Logger.Info("To email:"+ to);
               if (string.IsNullOrEmpty(to)) { return; }
               this.TemplatePath = templateParth;
               this.Parameters = parameters;

               EmailTemplateContent templateContent = TemplateReader.GetEmailTemplateContent(this.TemplatePath, this.Parameters);

               this.From = new MailAddress(!string.IsNullOrEmpty(templateContent.From) ? templateContent.From : from);
               string[] toemails = to.Split(';');
               if (toemails.Length > 1)
               {
                   foreach (string item in toemails)
                   {
                       try
                       {
                           if (string.IsNullOrEmpty(item)) { continue; }
                           this.Bcc.Add(item.Trim());
                       }
                       catch (Exception ex)
                       {
                           Logger.InfoWithParam("Error, email:'{0}', exception:{1}", item, ex);
                       }
                   }
               }
               else
               {
                   this.To.Add(to);
               }

               if (!string.IsNullOrEmpty(templateContent.Bcc))
               {
                   toemails = templateContent.Bcc.Split(';');
                   foreach (string item in toemails)
                   {
                       try
                       {
                           if (string.IsNullOrEmpty(item)) { continue; }
                           this.Bcc.Add(item.Trim());
                       }
                       catch (Exception ex)
                       {
                           Logger.InfoWithParam("Error, email:'{0}', exception:{1}", item, ex);
                       }
                   }
               }
               

               if (!string.IsNullOrEmpty(templateContent.CC))
               {
                   toemails = templateContent.CC.Split(';');
                   foreach (string item in toemails)
                   {
                       try
                       {
                           if (string.IsNullOrEmpty(item)) { continue; }
                           this.CC.Add(item.Trim());
                       }
                       catch (Exception ex)
                       {
                           Logger.InfoWithParam("Error, email:'{0}', exception:{1}", item, ex);
                       }
                   }
               }

               this.Subject = !string.IsNullOrEmpty(templateContent.Subject) ? templateContent.Subject : subject;
               this.Body = templateContent.Content;// TemplateReader.GetXmlContent(this.TemplatePath, this.Parameters);
               this.IsBodyHtml = true;
               //this.SubjectEncoding = this.BodyEncoding = Encoding.Unicode;
           }
           catch (Exception ex)
           {
               Logger.Error(ex);
               throw;
           }
       }
       
    }
}
