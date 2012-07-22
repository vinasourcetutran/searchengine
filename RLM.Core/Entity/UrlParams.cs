using System;
using System.Collections.Generic;
using System.Text;

namespace RLM.Core.Entity
{
    public class UrlParams:List<UrlParam>
    {
        public UrlParam this[UrlPramName item]
        {
            get
            {
                foreach (UrlParam url in this)
                {
                    if (url.Name.Equals(item.ToString(), StringComparison.OrdinalIgnoreCase)) { return url; }
                }
                return null;
            }
        }
    }
}
