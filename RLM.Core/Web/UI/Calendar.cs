using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading;

namespace RLM.Core.Web.UI
{
    [ParseChildren(true)]
    public class Calendar : Control, INamingContainer
    {
        #region Attribute
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";
        private HtmlGenericControl selectButton;
        private HtmlInputText selectedDateDisplayArea;
        private HtmlInputHidden selectedDateInputHidden;
        private DateTime selectedValue = DateTime.MinValue;
        private ITemplate selectTemplate;
        #endregion
        #region Properties
        public string DateTimeFormat
        {
            get
            {
                if (this.ViewState["DateFormat"] == null)
                {
                    return Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                }
                return (string)this.ViewState["DateFormat"];
            }
            set
            {
                this.ViewState["DateFormat"] = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return ((this.ViewState["Enabled"] == null) || ((bool)this.ViewState["Enabled"]));
            }
            set
            {
                this.ViewState["Enabled"] = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return (string)this.ViewState["ImageUrl"];
            }
            set
            {
                this.ViewState["ImageUrl"] = value;
            }
        }

        public DateTime SelectedValue
        {
            get
            {
                this.EnsureChildControls();
                if (this.selectedDateInputHidden.Value != "")
                {
                    this.selectedValue = DateTime.ParseExact(this.selectedDateInputHidden.Value, "yyyy-MM-dd HH:mm", null);
                }
                return this.selectedValue;
            }
            set
            {
                this.selectedValue = value;
            }
        }

        public ITemplate SelectTemplate
        {
            get
            {
                return this.selectTemplate;
            }
            set
            {
                this.selectTemplate = value;
            }
        }

        public bool ShowsTime
        {
            get
            {
                return ((this.ViewState["ShowsTime"] != null) && ((bool)this.ViewState["ShowsTime"]));
            }
            set
            {
                this.ViewState["ShowsTime"] = value;
            }
        }

        public int Size
        {
            get
            {
                if (this.ViewState["Size"] == null)
                {
                    return 10;
                }
                return (int)this.ViewState["Size"];
            }
            set
            {
                this.ViewState["Size"] = value;
            }
        }

        public string SupportFolder
        {
            get
            {
                if (this.ViewState["SupportFolder"] == null)
                {
                    return "~/JSCalendar";
                }
                return (string)this.ViewState["SupportFolder"];
            }
            set
            {
                this.ViewState["SupportFolder"] = value;
            }
        }

        public string Text
        {
            get
            {
                if (this.ViewState["Text"] == null)
                {
                    return "...";
                }
                return (string)this.ViewState["Text"];
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return (string)this.ViewState["ToolTip"];
            }
            set
            {
                this.ViewState["ToolTip"] = value;
            }
        }
        #endregion
        #region Methods
        private string ConvertDotNetDateTimeFormatToJSCalendarDateTimeFormat(string dateTimeFormat)
        {
            dateTimeFormat = Regex.Replace(dateTimeFormat, "dddd|ddd|dd|d|MMMM|MMM|MM|M|yyyy|yy|y|HH|H|hh|h|mm|m|ss|s|tt|t", new MatchEvaluator(this.ReplaceFormat));
            return dateTimeFormat;
        }

        protected override void CreateChildControls()
        {
            this.selectedDateInputHidden = new HtmlInputHidden();
            if (this.selectedValue != DateTime.MinValue)
            {
                this.selectedDateInputHidden.Value = this.selectedValue.ToString("yyyy-MM-dd HH:mm");
            }
            this.selectedDateInputHidden.ID = "selectedDateHidden";
            this.selectedDateDisplayArea = new HtmlInputText();
            this.selectedDateDisplayArea.Size = this.Size;
            if (this.Enabled)
            {
                this.selectedDateDisplayArea.Attributes.Add("readonly", "true");
            }
            else
            {
                this.selectedDateDisplayArea.Disabled = true;
            }
            this.selectedDateDisplayArea.ID = "dateTimeValue";
            this.Controls.Add(this.selectedDateInputHidden);
            this.Controls.Add(this.selectedDateDisplayArea);
            if (this.Enabled)
            {
                this.selectButton = new HtmlGenericControl("span");
                if (this.selectTemplate != null)
                {
                    this.selectTemplate.InstantiateIn(this.selectButton);
                }
                else
                {
                    this.selectButton.Attributes["title"] = this.ToolTip;
                    this.selectButton.InnerHtml = string.Format("<button>{0}</button>", HttpUtility.HtmlEncode(this.Text));
                }
                this.selectButton.Attributes.Add("title", HttpUtility.HtmlEncode(this.ToolTip));
                this.selectButton.ID = "selectButton";
                this.Controls.Add(this.selectButton);
            }
        }

        private string GetSupportScriptBlocks()
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}/calendar.js\"></script>\n<script type=\"text/javascript\" src=\"{0}/lang/calendar-{1}.js\"></script>\n<script type=\"text/javascript\" src=\"{0}/calendar-setup.js\"></script>\n", base.ResolveUrl(this.SupportFolder), Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.Page.IsClientScriptBlockRegistered("calendar"))
            {
                this.Page.RegisterClientScriptBlock("calendar", this.GetSupportScriptBlocks());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            if (this.SelectedValue != DateTime.MinValue)
            {
                this.selectedDateDisplayArea.Value = this.SelectedValue.ToString(this.DateTimeFormat);
            }
            base.Render(writer);
            if (this.Enabled)
            {
                this.RenderSetupScript(writer);
            }
        }

        private void RenderSetupScript(HtmlTextWriter writer)
        {
            string str = "";
            str = (((((str + "inputField : \"" + this.selectedDateInputHidden.ClientID + "\"") + ", ifFormat : \"%Y-%m-%d %H:%M\"") + ", displayArea : \"" + this.selectedDateDisplayArea.ClientID + "\"") + ", daFormat : \"" + this.ConvertDotNetDateTimeFormatToJSCalendarDateTimeFormat(this.DateTimeFormat) + "\"") + ", button : \"" + this.selectButton.ClientID + "\"") + ", showsTime : " + this.ShowsTime.ToString().ToLower();
            string str2 = "\n<script type=\"text/javascript\">\n    Calendar.setup({" + str + "});\n</script>\n";
            writer.Write(str2);
        }

        private string ReplaceFormat(Match m)
        {
            switch (m.Value)
            {
                case "dddd":
                    return "%A";

                case "ddd":
                    return "%a";

                case "dd":
                    return "%d";

                case "d":
                    return "%e";

                case "MMMM":
                    return "%B";

                case "MMM":
                    return "%b";

                case "M":
                case "MM":
                    return "%m";

                case "yyyy":
                    return "%Y";

                case "yy":
                case "y":
                    return "%y";

                case "HH":
                    return "%H";

                case "H":
                    return "%k";

                case "hh":
                    return "%I";

                case "h":
                    return "%l";

                case "mm":
                case "m":
                    return "%M";

                case "ss":
                case "s":
                    return "%S";

                case "tt":
                case "t":
                    return "%p";
            }
            return "";
        }
        #endregion

    }


}
