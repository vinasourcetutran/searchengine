using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace RLM.Core.Web.UI
{
    [ParseChildren(false), DefaultProperty("Text"), ToolboxData("<{0}:Label runat=\"server\"></{0}:Label>")]
    public class Label : WebControl
    {
        // Fields
        private string @for;
        private string text;

        // Methods
        public Label()
            : base(HtmlTextWriterTag.Label)
        {
            this.text = string.Empty;
            this.@for = string.Empty;
            this.EnableViewState = false;
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.@for))
            {
                Control control = this.NamingContainer.FindControl(this.@for);
                string str = (control != null) ? control.ClientID : this.@for;
                writer.AddAttribute("for", str);
            }
            base.AddAttributesToRender(writer);
        }

        // Properties
        public string For
        {
            get
            {
                return this.@for;
            }
            set
            {
                this.@for = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }


}
