using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public interface IEmailSender
    {
        // Methods
        void Send(EmailTemplate template, IEmailFormatter formatter, ParameterCollection parameterHash);
    }
}
