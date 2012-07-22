using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public interface IEmailFormatter
    {
        // Methods
        EmailTemplate Format(EmailTemplate template, ParameterCollection parameterHash);
    }

 

}
