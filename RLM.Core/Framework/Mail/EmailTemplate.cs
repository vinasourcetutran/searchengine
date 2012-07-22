using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace RLM.Core.Framework.Mail
{
    public class EmailTemplate
    {
        // Fields
        protected string bcc;
        protected string body;
        protected string bodyContentType;
        protected Encoding bodyEncoding;
        protected string cc;
        protected string from;
        private ICollection<LinkedResource> linkedResources;
        protected string subject;
        protected string to;

        // Methods
        public EmailTemplate()
        {
            this.linkedResources = new List<LinkedResource>();
        }

        public EmailTemplate(string subject, string from, string to, string body, string bodyContentType, Encoding bodyEncoding)
            : this(subject, from, to, body, bodyContentType, bodyEncoding, new List<LinkedResource>())
        {
        }

        public EmailTemplate(string subject, string from, string to, string body, string bodyContentType, Encoding bodyEncoding, ICollection<LinkedResource> linkedResources)
            : this(subject, from, to, string.Empty, string.Empty, body, bodyContentType, bodyEncoding, linkedResources)
        {
        }

        public EmailTemplate(string subject, string from, string to, string cc, string bcc, string body, string bodyContentType, Encoding bodyEncoding, ICollection<LinkedResource> linkedResources)
        {
            this.linkedResources = new List<LinkedResource>();
            this.subject = subject;
            this.from = from;
            this.to = to;
            this.cc = cc;
            this.bcc = bcc;
            this.body = body;
            this.bodyEncoding = bodyEncoding;
            this.bodyContentType = bodyContentType;
            this.linkedResources = linkedResources;
        }

        public override string ToString()
        {
            return string.Format("\r\n                From: {0}\r\n                To: {1}\r\n                Cc: {2}\r\n                Bcc: {3}\r\n                Subject: {4}\r\n                Body Content Type: {5}\r\n                Body Encoding : {6}    \r\n                Body: {7}", new object[] { this.From, this.To, this.Cc, this.Bcc, this.Subject, this.BodyContentType, this.BodyEncoding, this.Body });
        }

        // Properties
        public string Bcc
        {
            get
            {
                return this.bcc;
            }
        }

        public string Body
        {
            get
            {
                return this.body;
            }
        }

        public string BodyContentType
        {
            get
            {
                return this.bodyContentType;
            }
        }

        public Encoding BodyEncoding
        {
            get
            {
                return this.bodyEncoding;
            }
        }

        public string Cc
        {
            get
            {
                return this.cc;
            }
        }

        public string From
        {
            get
            {
                return this.from;
            }
        }

        public ICollection<LinkedResource> LinkedResources
        {
            get
            {
                return this.linkedResources;
            }
        }

        public string Subject
        {
            get
            {
                return this.subject;
            }
        }

        public string To
        {
            get
            {
                return this.to;
            }
        }
    }


}
