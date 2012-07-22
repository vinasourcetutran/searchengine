using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public class BaseMasterPage : System.Web.UI.MasterPage, IPage
    {
        #region Properties
        public bool IsSetMetadata { get; set; }
        public virtual string PageTitle
        {
            get;
            set;
        }
        public virtual string Keyword
        {
            get;
            set;
        }
        public virtual string Description
        {
            get;
            set;
        }
        public virtual string Author
        {
            get;
            set;
        }
        #endregion

        #region Event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnCustomLoad(e);
        }
        #endregion

        #region Override Methods
        public virtual void OnCustomLoad(EventArgs e){}
        #endregion
    }
}
