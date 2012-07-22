using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace RLM.Core.Web.UI
{
    [PersistChildren(true), ToolboxData("<{0}:Image runat=server></{0}:Image>"), ParseChildren(false)]
    public class Image : WebControl
    {
        // Methods
        public Image()
            : base("img")
        {
            this.EnableViewState = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Src != null)
            {
                this.Src = base.ResolveUrl(this.Src);
            }
            base.Render(writer);
        }

        // Properties
        [Category("Appearance"), DefaultValue(""), Bindable(true)]
        public string Src
        {
            get
            {
                return base.Attributes["src"];
            }
            set
            {
                base.Attributes["src"] = value;
            }
        }
    }

 

}
