using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public class EmailTemplateContent
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
    }
}
