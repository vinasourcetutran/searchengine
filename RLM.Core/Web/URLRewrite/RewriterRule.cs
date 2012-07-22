using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.URLRewrite
{
    [Serializable]
    public class RewriterRule
    {
        // Fields
        private string lookFor;
        private string sendTo;

        // Properties
        public string LookFor
        {
            get
            {
                return this.lookFor;
            }
            set
            {
                this.lookFor = value;
            }
        }

        public string SendTo
        {
            get
            {
                return this.sendTo;
            }
            set
            {
                this.sendTo = value;
            }
        }
    }


}
