using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    class InvalidEmailTemplateException:Exception
    {
        public InvalidEmailTemplateException(string message)
            : base(message)
        {
        }

        public InvalidEmailTemplateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
