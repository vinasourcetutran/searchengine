using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace RLM.Core.Web.UI
{
    public class CustomHyperLinkPager : CustomBasePager
    {
        // Methods
        protected override Control CreateFirstPaginatorControl()
        {
            System.Web.UI.WebControls.HyperLink link = new System.Web.UI.WebControls.HyperLink();
            link.ID = "firstLink";
            link.Text = base.FirstButtonText;
            link.ToolTip = base.FirstButtonToolTip;
            link.NavigateUrl = this.GetLink(0);
            //link.CssClass = "";
            return link;
        }

        protected override Control CreateLastPaginatorControl()
        {
            System.Web.UI.WebControls.HyperLink link = new System.Web.UI.WebControls.HyperLink();
            link.ID = "lastLink";
            link.Text = base.LastButtonText;
            link.ToolTip = base.LastButtonToolTip;
            link.NavigateUrl = this.GetLink(base.TotalPages - 1);
            return link;
        }

        protected override Control[] CreateMainPaginatorControls(int fromPage, int toPage)
        {
            System.Web.UI.WebControls.HyperLink[] linkArray = new System.Web.UI.WebControls.HyperLink[toPage - fromPage];
            for (int i = 0; i < linkArray.Length; i++)
            {
                linkArray[i] = new System.Web.UI.WebControls.HyperLink();
                linkArray[i].EnableViewState = false;
                if (base.PageButtonTextFormat == null)
                {
                    linkArray[i].Text = ((i + fromPage) + 1).ToString();
                }
                else
                {
                    linkArray[i].Text = string.Format(base.PageButtonTextFormat, (i + fromPage) + 1);
                }
                linkArray[i].ID = (i + fromPage).ToString();
                if ((i + fromPage) != base.PageIndex)
                {
                    linkArray[i].NavigateUrl = this.GetLink(i + fromPage);
                    linkArray[i].Attributes.Add("onclick", string.Format("reloadRepeater({0}); return false;", i + fromPage));                    
                }
                else
                {
                    linkArray[i].CssClass = "current";
                }
            }
            return linkArray;
        }

        protected void RefreshPager(int selectPage)
        {
            ViewState["SelectedPageIndex"] = selectPage;
        }

        protected override Control CreateNextPaginatorControl()
        {
            
            System.Web.UI.WebControls.HyperLink link = new System.Web.UI.WebControls.HyperLink();
            link.ID = "nextLink";
            link.Text = base.NextButtonText;
            link.ToolTip = base.NextButtonToolTip;
            link.NavigateUrl = this.GetLink(base.PageIndex + 1);
            link.Attributes.Add("onclick", string.Format("reloadRepeater({0}); return false;", this.PageIndex + 1));
            if (base.HasNext)
            {
                link.CssClass = "nextBtn02";
            }
            return link;
        }

        protected override Control CreatePreviousPaginatorControl()
        {
            System.Web.UI.WebControls.HyperLink link = new System.Web.UI.WebControls.HyperLink();
            link.ID = "previousLink";
            link.Text = base.PreviousButtonText;
            link.ToolTip = base.PreviousButtonToolTip;
            link.NavigateUrl = this.GetLink(base.PageIndex - 1);
            if (!IsRedirect)
            {
                link.Attributes.Add("onclick", string.Format("reloadRepeater({0}); return false;", this.PageIndex - 1));
            }

            if (base.HasPrevious)
            {
                link.CssClass = "prevBtn02";
            }
            return link;
        }

        private string GetLink(int page)
        {
            string rawUrl = this.Page.Request.RawUrl;
            Regex regex = new Regex("^(.*)(" + this.Parameter + @")=(\d+)(&?.*)$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(rawUrl) && (this.BaseUrl == null))
            {
                return regex.Replace(this.Page.Request.RawUrl, "$1$2=" + (page + 1) + "$4");
            }
            if (this.BaseUrl != null)
            {
                rawUrl = this.BaseUrl;
            }
            string str2 = "&";
            if (rawUrl.IndexOf("?") < 0)
            {
                str2 = "?";
            }
            return string.Format("{0}{1}{2}={3}", new object[] { rawUrl, str2, this.Parameter, page + 1 });
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Parameter == null)
            {
                throw new ArgumentNullException("Parameter");
            }
            try
            {
                base.PageIndex = Convert.ToInt32(this.Page.Request.QueryString[this.Parameter]) - 1;
            }
            catch
            {
            }
        }

        // Properties
        public string BaseUrl
        {
            get
            {
                return (string)this.ViewState["BaseUrl"];
            }
            set
            {
                this.ViewState["BaseUrl"] = value;
            }
        }

        public string Parameter
        {
            get
            {
                try
                {
                    return (string)this.ViewState["PagingParameter"];
                }
                catch { }
                return "page";
            }
            set
            {
                this.ViewState["PagingParameter"] = value;
            }
        }

        public bool IsRedirect
        {
            get
            {
                try
                {
                    return (bool)this.ViewState["IsRedirect"];
                }
                catch { }
                return true;
            }
            set
            {
                this.ViewState["IsRedirect"] = value;
            }
        }
    }
}
