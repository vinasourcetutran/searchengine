using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections.Specialized;
using RLM.Core.Framework.Utility;

namespace RLM.Core.Web.UI
{
    public abstract class AbstractValidationRule : IValidationRule
    {
        // Fields
        private NameValueCollection config = new NameValueCollection();
        private Control controlToValidate;
        private string errorMessage;
        private string hint;

        // Methods
        public AbstractValidationRule(Control controlToValidate, string errorMessage, string hint)
        {
            this.controlToValidate = controlToValidate;
            this.errorMessage = errorMessage;
            this.hint = hint;
        }

        protected virtual void AddConfigsToRender()
        {
        }

        protected void AddObject(string configName, object value)
        {
            this.config.Add(configName, value.ToString());
        }

        protected void AddString(string configName, string value)
        {
            this.config.Add(configName, "\"" + JavascriptUtility.EscapeString(value) + "\"");
        }

        public string RenderClientConstructorScript()
        {
            this.AddString("field", this.ControlToValidate.UniqueID);
            this.AddString("message", this.ErrorMessage);
            this.AddString("hint", this.Hint);
            this.AddConfigsToRender();
            string str = "";
            foreach (string str2 in this.config.Keys)
            {
                string str3 = str;
                str = str3 + str2 + " : " + this.config[str2] + ", ";
            }
            if (str.EndsWith(", "))
            {
                str = str.Substring(0, str.Length - 2);
            }
            if (str.Length > 0)
            {
                str = "{" + str + "}";
            }
            return string.Format("new {0}({1})", this.Name, str);
        }

        public abstract ValidationError Validate();

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

        public abstract string Name { get; }
    }


}
