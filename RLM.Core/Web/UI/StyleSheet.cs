using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;

namespace RLM.Core.Web.UI
{
    using System.Configuration;
    using RLM.Core.Framework.Utility;

    [DefaultProperty("Text"), ToolboxData("<{0}:StyleSheet runat=server></{0}:StyleSheet>")]
    public class StyleSheet : WebControl
    {
        #region Variables
        string baseUrl = string.Empty;
        bool isRemoveCache = false;
        #endregion

        #region  Construct
        public StyleSheet()
            : base("link")
        {
            base.Attributes["rel"] = "stylesheet";
            base.Attributes["type"] = "text/css";
            this.EnableViewState = false;
        }
        #endregion

        #region Properties

        [DefaultValue(""), Bindable(true), Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), UrlProperty]
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

        [DefaultValue(""), Bindable(true), UrlProperty]
        public string BaseUrl
        {
            get
            {
                string tempBaseUrl = ConfigurationManager.AppSettings["RLMCore.BaseStyleSheetUrl"];
                if(!string.IsNullOrEmpty(tempBaseUrl))
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

        [DefaultValue(""), Bindable(true), UrlProperty]
        public bool IsRemoveCache
        {
            get
            {
                return this.isRemoveCache;
            }
            set
            {
                this.isRemoveCache = value;
            }
        }

        [DefaultValue(""), Bindable(true), UrlProperty]
        public string Media
        {
            get
            {
                return this.Attributes["media"];
            }
            set
            {
                this.Attributes["media"] = value;
            }
        }

        #endregion

        #region Render methods
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Href != null)
            {
                this.Href = !string.IsNullOrEmpty(this.BaseUrl) ? string.Format("{0}{1}", this.BaseUrl, this.Href) : this.Href;
                this.Href = base.ResolveUrl(this.Href);
                if (this.isRemoveCache)
                {
                    this.Href = this.Href + "?id=" + StringHelper.GetGuid();

                }
            }
            writer.WriteBeginTag(this.TagName);
            foreach (string str in base.Attributes.Keys)
            {
                writer.WriteAttribute(str, base.Attributes[str]);
            }
            writer.Write(" />");
        }
        #endregion
    }
}
