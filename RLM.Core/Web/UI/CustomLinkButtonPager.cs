using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace RLM.Core.Web.UI
{
    public class CustomLinkButtonPager : CustomBasePager
    {
        // Methods
        protected override Control CreateFirstPaginatorControl()
        {
            LinkButton link = new LinkButton();
            link.ID = "firstLink";
            link.Text = base.FirstButtonText;
            link.ToolTip = base.FirstButtonToolTip;
            //link.NavigateUrl = this.GetLink(0);
            //link.CssClass = "";
            
            return link;
        }

        protected override Control CreateLastPaginatorControl()
        {
            LinkButton link = new LinkButton();
            link.ID = "lastLink";
            link.Text = base.LastButtonText;
            link.ToolTip = base.LastButtonToolTip;
            //link.NavigateUrl = this.GetLink(base.TotalPages - 1);
            return link;
        }

        protected override Control[] CreateMainPaginatorControls(int fromPage, int toPage)
        {
            LinkButton[] linkArray = new LinkButton[toPage - fromPage];
            for (int i = 0; i < linkArray.Length; i++)
            {
                linkArray[i] = new LinkButton();
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
                    linkArray[i].Attributes.Add("onClick", string.Format("reloadRepeater({0});", i + fromPage));                    
                }
                else
                {
                    linkArray[i].CssClass = "current";
                }
            }
            return linkArray;
        }

        protected override Control CreateNextPaginatorControl()
        {
            LinkButton link = new LinkButton();
            link.ID = "nextLink";
            if (base.HasNext)
            {
                link.Text = base.NextButtonText;
                link.ToolTip = base.NextButtonToolTip;
                link.Attributes.Add("onClick", string.Format("reloadRepeater({0});", this.PageIndex + 1));
                link.CssClass = "nextBtn02";
            }
            //else
            //{
            //    link.Text = "&nbsp;";
            //}
            return link;
        }

        protected override Control CreatePreviousPaginatorControl()
        {
            LinkButton link = new LinkButton();
            link.ID = "previousLink";
            if (base.HasPrevious)
            {
                link.Text = base.PreviousButtonText;
                link.ToolTip = base.PreviousButtonToolTip;
                link.Attributes.Add("onClick", string.Format("reloadRepeater({0});", this.PageIndex - 1));
                link.CssClass = "prevBtn02";
            }
            //else
            //{
            //    link.Text = "&nbsp;";
            //}
            return link;
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
                base.PageIndex = Convert.ToInt32(ViewState[this.Parameter]) - 1;
            }
            catch
            {
            }
        }

        public string Parameter
        {
            get
            {
                return (string)this.ViewState["PagingParameter"];
            }
            set
            {
                this.ViewState["PagingParameter"] = value;
            }
        }       

    }
}
