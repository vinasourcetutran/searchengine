using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public class ParameterCollection
    {
        // Fields
        private bool autoHtmlEncode;
        private List<Parameter> parameters;

        // Methods
        public ParameterCollection()
            : this(false)
        {
        }

        public ParameterCollection(bool autoHtmlEncode)
        {
            this.autoHtmlEncode = autoHtmlEncode;
            this.parameters = new List<Parameter>();
        }

        public void Add(string name, object value)
        {
            this.parameters.Add(new Parameter(name, value, this.autoHtmlEncode));
        }

        public void Add(string name, object value, bool htmlEncode)
        {
            this.parameters.Add(new Parameter(name, value, htmlEncode));
        }

        public IEnumerator<Parameter> GetEnumerator()
        {
            return this.parameters.GetEnumerator();
        }
    }


}
