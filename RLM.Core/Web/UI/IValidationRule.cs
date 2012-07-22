using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public interface IValidationRule
    {
        // Methods
        string RenderClientConstructorScript();
        ValidationError Validate();

        // Properties
        string Name { get; }
    }

 

 

}
