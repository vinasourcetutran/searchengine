using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using RLM.Core.Framework.Utility;

namespace RLM.Core.Web.UI
{
    public class ValidationError
    {
        // Fields
        private Control controlToValidate;
        private string errorMessage;
        private string hint;
        private IValidationRule validationRule;

        // Methods
        public ValidationError(Control controlToValidate, string errorMessage, string hint, IValidationRule validationRule)
        {
            this.controlToValidate = controlToValidate;
            this.errorMessage = errorMessage;
            this.hint = hint;
            this.validationRule = validationRule;
        }

        public string RenderClientConstructorScript()
        {
            return string.Format("new ValidationError(document.getElementById('{0}'), \"{1}\", \"{2}\", null)", this.controlToValidate.ClientID, JavascriptUtility.EscapeString(this.errorMessage), JavascriptUtility.EscapeString(this.hint));
        }

        // Properties
        public Control ControlToValidate
        {
            get
            {
                return this.controlToValidate;
            }
            set
            {
                this.controlToValidate = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }

        public string Hint
        {
            get
            {
                return this.hint;
            }
            set
            {
                this.hint = value;
            }
        }

        public IValidationRule ValidationRule
        {
            get
            {
                return this.validationRule;
            }
            set
            {
                this.validationRule = value;
            }
        }
    }


}
