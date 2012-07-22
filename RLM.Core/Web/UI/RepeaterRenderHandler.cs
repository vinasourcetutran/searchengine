using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;

namespace RLM.Core.Web.UI
{
    /// <summary>
    /// Summary description for IRepeater
    /// </summary>
    public class RepeaterRenderHandler
    {
        #region Constructor
        public RepeaterRenderHandler()
        {

            this.Data = new Hashtable();
        }
        #endregion
        #region Properties
        public Hashtable Data { get; set; }
        #endregion
        #region Methods
        public virtual string HeaderRender(RepeaterItemEventArgs e) { return string.Empty; }
        public virtual string ItemRender(RepeaterItemEventArgs e, object activeItemId) { return string.Empty; }
        public virtual string AlternativeItemRender(RepeaterItemEventArgs e, object activeItemId) { return this.ItemRender(e, activeItemId); }
        public virtual string ItemRender(RepeaterItemEventArgs e, int totalItems, object activeItemId) { return this.ItemRender(e, activeItemId); }
        public virtual string AlternativeItemRender(RepeaterItemEventArgs e, int totalItems, object activeItemId) { return this.ItemRender(e, totalItems, activeItemId); }
        public virtual string FooterRender(RepeaterItemEventArgs e) { return string.Empty; }
        #endregion
    }

}
