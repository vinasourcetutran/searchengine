using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing.Design;

namespace RLM.Core.Web.UI
{
    using System.Configuration;

    [DefaultProperty("Text"), ToolboxData("<{0}:JavaScript runat=\"server\"></{0}:JavaScript>")]
    public class JavaScript : WebControl
    {
        #region Variables
        string baseUrl = string.Empty;
        #endregion

        #region Constructor
        public JavaScript()
            : base("script")
        {
            this.Language = "JavaScript";
            this.Type = "text/javascript";
            //this.Async = "false";
            this.EnableViewState = false;
        }
        #endregion

        #region Properties
        public string Type
        {
            get
            {
                return base.Attributes["type"];
            }
            set
            {
                base.Attributes["type"] = value;
            }
        }

        public string Async
        {
            get
            {
                return base.Attributes["async"];
            }
            set
            {
                base.Attributes["async"] = value;
            }
        }

        // Type of script. i.e: text/javascript, text/html,...
        public string Language
        {
            get
            {
                return base.Attributes["language"];
            }
            set
            {
                base.Attributes["language"] = value;
            }
        }
        // path to script file. It can be relate or absolute path
        [DefaultValue(""), Bindable(true), Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), UrlProperty]
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
        // javascript function that was called if error to load script file
        public string OnError
        {
            get
            {
                return base.Attributes["onerror"];
            }
            set
            {
                base.Attributes["onerror"] = value;
            }
        }

        public string BaseUrl
        {
            get
            {
                string tempBaseUrl = ConfigurationManager.AppSettings["RLMCore.BaseJavaScriptUrl"];
                if (!string.IsNullOrEmpty(tempBaseUrl))
                {
                    this.baseUrl = tempBaseUrl;
                }
                return this.baseUrl;
            }
            set
            {
                this.baseUrl = value;
            }
        }
        #endregion

        #region Methods
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Src != null)
            {
                this.Src = !string.IsNullOrEmpty(this.BaseUrl) ? string.Format("{0}{1}", this.BaseUrl, this.Src) : this.Src;
                this.Src = base.ResolveUrl(this.Src);
            }
            writer.WriteBeginTag(this.TagName);
            foreach (string str in base.Attributes.Keys)
            {
                writer.WriteAttribute(str, base.Attributes[str]);
            }
            writer.Write('>');
            writer.WriteEndTag(this.TagName);
        }
        #endregion
    }


}
