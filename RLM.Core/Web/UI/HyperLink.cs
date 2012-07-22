using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace RLM.Core.Web.UI
{
    [ParseChildren(false), ToolboxData("<{0}:HyperLink runat=server></{0}:HyperLink>"), PersistChildren(true)]
    public class HyperLink : WebControl
    {
        // Methods
        public HyperLink()
            : base("a")
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
        [Bindable(true), DefaultValue(""), Category("Appearance")]
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
