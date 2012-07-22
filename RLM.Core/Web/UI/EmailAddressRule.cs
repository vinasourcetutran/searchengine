using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace RLM.Core.Web.UI
{
    public class EmailAddressRule : PatternMatchedRule
    {
        // Methods
        public EmailAddressRule(Control controlToValidate, string errorMessage)
            : this(controlToValidate, errorMessage, "")
        {
        }

        public EmailAddressRule(Control controlToValidate, string errorMessage, string hint)
            : base(controlToValidate, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", errorMessage, hint)
        {
        }
    }

 

}
