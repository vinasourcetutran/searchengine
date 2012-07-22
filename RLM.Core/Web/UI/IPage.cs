using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public interface IPage
    {
        string PageTitle
        {
            get;
            set;
        }
        string Keyword
        {
            get;
            set;
        }
        string Description
        {
            get;
            set;
        }
        string Author
        {
            get;
            set;
        }
    }
}
