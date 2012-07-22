using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public class Parameter
    {
        // Fields
        private bool htmlEncode;
        private string name;
        private object value;

        // Methods
        public Parameter(string name, object value)
            : this(name, value, false)
        {
        }

        public Parameter(string name, object value, bool htmlEncode)
        {
            this.name = name;
            this.value = value;
            this.htmlEncode = htmlEncode;
        }

        // Properties
        public bool HtmlEncode
        {
            get
            {
                return this.htmlEncode;
            }
            set
            {
                this.htmlEncode = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }


}
