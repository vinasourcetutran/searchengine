using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public class BasePage : System.Web.UI.Page
    {
        #region Variable
        IPage masterPage=null;
        #endregion
        #region Properties
        public virtual IPage MasterPage
        {
            get
            {
                if (masterPage == null && this.Master!=null)
                {
                    masterPage = (IPage)this.Master;
                }
                return masterPage;
            }
        }
        #endregion
    }
}
