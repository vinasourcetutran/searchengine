using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace RLM.Core.Web.UI
{
    [ToolboxData("<{0}:Link runat=server></{0}:Link>"), PersistChildren(true), ParseChildren(false)]
    public class Link : WebControl
    {
        // Methods
        public Link()
            : base("link")
        {
            this.EnableViewState = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Href != null)
            {
                this.Href = base.ResolveUrl(this.Href);
            }
            base.Render(writer);
        }

        // Properties
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Href
        {
            get
            {
                return base.Attributes["href"];
            }
            set
            {
                base.Attributes["href"] = value;
            }
        }
    }


}
