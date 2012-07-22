using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RLM.Core.Web.URLRewrite
{
    [Serializable]
    public class RewriterRuleCollection : CollectionBase
    {
        // Methods
        public virtual void Add(RewriterRule r)
        {
            base.InnerList.Add(r);
        }

        // Properties
        public RewriterRule this[int index]
        {
            get
            {
                return (RewriterRule)base.InnerList[index];
            }
            set
            {
                base.InnerList[index] = value;
            }
        }
    }


}
