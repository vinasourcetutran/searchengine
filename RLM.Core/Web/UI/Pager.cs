using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

namespace RLM.Core.Web.UI
{
    public delegate void PagerEventHandler(object sender, PagerEventArgs e);
    public class Pager : Label, INamingContainer
    {

        #region Properties
        private bool _causeValidation = true;
        public const int DEFAULT_MAX_DISPLAY_PAGES = 10;
        public const int DEFAULT_PAGE_SIZE = 20;
        private LinkButton firstButton;
        private LinkButton lastButton;
        private LinkButton nextButton;
        private DropDownList pagingDropDownList;
        private LinkButton[] pagingLinkButtons;
        private LinkButton previousButton;
        // event
        public event EventHandler IndexChanged;
        public event PagerEventHandler PagerIndexChange;
        #endregion

        #region Methods
        private void AddFirstLastLinkButtons()
        {
            this.firstButton = new LinkButton();
            this.firstButton.ID = "firstButton";
            this.firstButton.Text = this.FirstButtonText;
            this.firstButton.ToolTip = this.FirstButtonToolTip;
            this.firstButton.CommandArgument = "0";
            this.firstButton.Click += new EventHandler(this.PageIndex_Click);
            this.firstButton.CausesValidation = this.CausesValidation;
            this.Controls.Add(this.firstButton);
            this.lastButton = new LinkButton();
            this.lastButton.ID = "lastButton";
            this.lastButton.Text = this.LastButtonText;
            this.lastButton.ToolTip = this.LastButtonToolTip;
            this.lastButton.CommandArgument = (this.CalculateTotalPages() - 1).ToString();
            this.lastButton.Click += new EventHandler(this.PageIndex_Click);
            this.lastButton.CausesValidation = this.CausesValidation;
            this.Controls.Add(this.lastButton);
        }

        private void AddPageButtons()
        {
            this.pagingLinkButtons = new LinkButton[this.CalculateTotalPages()];
            for (int i = 0; i < this.pagingLinkButtons.Length; i++)
            {
                this.pagingLinkButtons[i] = new LinkButton();
                this.pagingLinkButtons[i].EnableViewState = false;
                if (this.PageButtonTextFormat == null)
                {
                    this.pagingLinkButtons[i].Text = (i + 1).ToString();
                }
                else
                {
                    this.pagingLinkButtons[i].Text = string.Format(this.PageButtonTextFormat, i + 1);
                }
                this.pagingLinkButtons[i].ID = i.ToString();
                this.pagingLinkButtons[i].CommandArgument = i.ToString();
                this.pagingLinkButtons[i].Click += new EventHandler(this.PageIndex_Click);
                this.pagingLinkButtons[i].CausesValidation = this.CausesValidation;
                this.Controls.Add(this.pagingLinkButtons[i]);
            }
        }

        private void AddPageDropDownList()
        {
            this.pagingDropDownList = new DropDownList();
            this.pagingDropDownList.ID = "dropDownList";
            this.pagingDropDownList.AutoPostBack = true;
            this.pagingDropDownList.SelectedIndex = this.PageIndex;
            this.pagingDropDownList.SelectedIndexChanged += new EventHandler(this.PagingDropDownList_SelectedIndexChanged);
            int capacity = this.CalculateTotalPages();
            ArrayList list = new ArrayList(capacity);
            for (int i = 0; i < capacity; i++)
            {
                list.Add((i + 1).ToString());
            }
            this.pagingDropDownList.DataSource = list;
            this.pagingDropDownList.DataBind();
            this.Controls.Add(this.pagingDropDownList);
        }

        private void AddPreviousNextLinkButtons()
        {
            this.previousButton = new LinkButton();
            this.previousButton.ID = "previousButton";
            this.previousButton.Text = this.PreviousButtonText;
            this.previousButton.ToolTip = this.PreviousButtonToolTip;
            this.previousButton.CommandArgument = (this.PageIndex - 1).ToString();
            this.previousButton.Click += new EventHandler(this.PageIndex_Click);
            this.previousButton.CausesValidation = this.CausesValidation;
            this.Controls.Add(this.previousButton);
            this.nextButton = new LinkButton();
            this.nextButton.ID = "nextButton";
            this.nextButton.Text = this.NextButtonText;
            this.nextButton.ToolTip = this.NextButtonToolTip;
            this.nextButton.CommandArgument = (this.PageIndex + 1).ToString();
            this.nextButton.Click += new EventHandler(this.PageIndex_Click);
            this.nextButton.CausesValidation = this.CausesValidation;
            this.Controls.Add(this.nextButton);
        }

        public int CalculateTotalPages()
        {
            if (this.TotalRecords == 0)
            {
                return 0;
            }
            int num = this.TotalRecords / this.PageSize;
            if ((this.TotalRecords % this.PageSize) > 0)
            {
                num++;
            }
            return num;
        }
        private void RenderButtonRange(int start, int end, HtmlTextWriter writer)
        {
            for (int i = start; i < end; i++)
            {
                if (this.PageIndex == i)
                {
                    Literal literal = new Literal();
                    if (this.PageButtonTextFormat == null)
                    {
                        literal.Text = (i + 1).ToString();
                    }
                    else
                    {
                        literal.Text = string.Format(this.PageButtonTextFormat, i + 1);
                    }
                    literal.RenderControl(writer);
                }
                else
                {
                    this.pagingLinkButtons[i].RenderControl(writer);
                }
                if (i < (end - 1))
                {
                    writer.Write(" ");
                }
            }
        }

        private void RenderDropDownListMode(HtmlTextWriter writer)
        {
            this.RenderPrevious(writer);
            this.RenderPagingDropDownList(writer);
            this.RenderNext(writer);
        }

        private void RenderFirst(HtmlTextWriter writer)
        {
            int num = this.CalculateTotalPages();
            if ((this.PageIndex > (this.MaxDisplayPages / 2)) && (num > this.MaxDisplayPages))
            {
                this.firstButton.RenderControl(writer);
                new LiteralControl("&nbsp;...&nbsp;").RenderControl(writer);
            }
        }

        private void RenderLast(HtmlTextWriter writer)
        {
            int num = this.CalculateTotalPages();
            if ((((this.PageIndex + (this.MaxDisplayPages / 2)) + (this.MaxDisplayPages % 2)) < num) && (num > this.MaxDisplayPages))
            {
                new LiteralControl("&nbsp;...&nbsp;").RenderControl(writer);
                this.lastButton.RenderControl(writer);
            }
        }

        private void RenderNext(HtmlTextWriter writer)
        {
            if (this.HasNext)
            {
                Literal literal = new Literal();
                literal.Text = "&nbsp;";
                literal.RenderControl(writer);
                this.nextButton.RenderControl(writer);
            }
        }

        private void RenderNumericMode(HtmlTextWriter writer)
        {
            this.RenderFirst(writer);
            this.RenderPrevious(writer);
            this.RenderPagingButtons(writer);
            this.RenderNext(writer);
            this.RenderLast(writer);
        }

        private void RenderPagingButtons(HtmlTextWriter writer)
        {
            int end = this.CalculateTotalPages();
            if (end <= this.MaxDisplayPages)
            {
                this.RenderButtonRange(0, end, writer);
            }
            else
            {
                int start = this.PageIndex - (this.MaxDisplayPages / 2);
                int num3 = (this.PageIndex + (this.MaxDisplayPages / 2)) + (this.MaxDisplayPages % 2);
                if (start <= 0)
                {
                    start = 0;
                    num3 = start + this.MaxDisplayPages;
                }
                if (num3 > end)
                {
                    num3 = end;
                    start = end - this.MaxDisplayPages;
                }
                this.RenderButtonRange(start, num3, writer);
            }
        }

        private void RenderPagingDropDownList(HtmlTextWriter writer)
        {
            this.pagingDropDownList.SelectedIndex = this.PageIndex;
            this.pagingDropDownList.RenderControl(writer);
        }

        private void RenderPrevious(HtmlTextWriter writer)
        {
            if (this.HasPrevious)
            {
                this.previousButton.RenderControl(writer);
                Literal literal = new Literal();
                literal.Text = "&nbsp;";
                literal.RenderControl(writer);
            }
        }

        private void RenderPrevNextMode(HtmlTextWriter writer)
        {
            this.RenderPrevious(writer);
            this.RenderNext(writer);
        }
        #endregion

        #region Properties
        public bool CausesValidation
        {
            get
            {
                return this._causeValidation;
            }
            set
            {
                this._causeValidation = value;
            }
        }

        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        public string FirstButtonText
        {
            get
            {
                if (this.ViewState["FirstButtonText"] == null)
                {
                    return "";
                }
                return (string)this.ViewState["FirstButtonText"];
            }
            set
            {
                this.ViewState["FirstButtonText"] = value;
            }
        }

        public string FirstButtonToolTip
        {
            get
            {
                return (string)this.ViewState["FirstButtonToolTip"];
            }
            set
            {
                this.ViewState["FirstButtonToolTip"] = value;
            }
        }

        private bool HasNext
        {
            get
            {
                return ((this.PageIndex + 1) < this.CalculateTotalPages());
            }
        }

        private bool HasPrevious
        {
            get
            {
                return (this.PageIndex > 0);
            }
        }

        public string LastButtonText
        {
            get
            {
                if (this.ViewState["LastButtonText"] == null)
                {
                    return "";
                }
                return (string)this.ViewState["LastButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["LastButtonText"] = "";
                }
                else
                {
                    this.ViewState["LastButtonText"] = value;
                }
            }
        }

        public string LastButtonToolTip
        {
            get
            {
                return (string)this.ViewState["LastButtonToolTip"];
            }
            set
            {
                this.ViewState["LastButtonToolTip"] = value;
            }
        }

        public int MaxDisplayPages
        {
            get
            {
                if (this.ViewState["MaxDisplayPages"] == null)
                {
                    return 10;
                }
                return (int)this.ViewState["MaxDisplayPages"];
            }
            set
            {
                this.ViewState["MaxDisplayPages"] = value;
            }
        }

        public string NextButtonText
        {
            get
            {
                if (this.ViewState["NextButtonText"] == null)
                {
                    return "&gt;&gt;";
                }
                return (string)this.ViewState["NextButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["NextButtonText"] = "&gt;&gt;";
                }
                else
                {
                    this.ViewState["NextButtonText"] = value;
                }
            }
        }

        public string NextButtonToolTip
        {
            get
            {
                return (string)this.ViewState["NextButtonToolTip"];
            }
            set
            {
                this.ViewState["NextButtonToolTip"] = value;
            }
        }

        public string PageButtonTextFormat
        {
            get
            {
                return (string)this.ViewState["PageButtonTextFormat"];
            }
            set
            {
                this.ViewState["PageButtonTextFormat"] = value;
            }
        }

        public int PageIndex
        {
            get
            {
                int num = 0;
                num = Convert.ToInt32(this.ViewState["PageIndex"]);
                if (num < 0)
                {
                    return 0;
                }
                return num;
            }
            set
            {
                this.ViewState["PageIndex"] = value;
            }
        }

        public int PageSize
        {
            get
            {
                int num = Convert.ToInt32(this.ViewState["PageSize"]);
                if (num == 0)
                {
                    num = 20;
                }
                return num;
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        public string PreviousButtonText
        {
            get
            {
                if (this.ViewState["PreviousButtonText"] == null)
                {
                    return "&lt;&lt;";
                }
                return (string)this.ViewState["PreviousButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["PreviousButtonText"] = "&lt;&lt;";
                }
                else
                {
                    this.ViewState["PreviousButtonText"] = value;
                }
            }
        }

        public string PreviousButtonToolTip
        {
            get
            {
                return (string)this.ViewState["PreviousButtonToolTip"];
            }
            set
            {
                this.ViewState["PreviousButtonToolTip"] = value;
            }
        }

        public PagerRenderMode RenderMode
        {
            get
            {
                if (this.ViewState["RenderMode"] == null)
                {
                    return PagerRenderMode.Numeric;
                }
                return (PagerRenderMode)this.ViewState["RenderMode"];
            }
            set
            {
                this.ViewState["RenderMode"] = value;
            }
        }

        public int TotalPages
        {
            get
            {
                return this.CalculateTotalPages();
            }
        }

        public int TotalRecords
        {
            get
            {
                return Convert.ToInt32(this.ViewState["TotalRecords"]);
            }
            set
            {
                this.ViewState["TotalRecords"] = value;
            }
        }
        #endregion

        #region Render methods
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            switch (this.RenderMode)
            {
                case PagerRenderMode.DropDownList:
                    this.AddPageDropDownList();
                    break;

                default:
                    this.AddPageButtons();
                    this.AddFirstLastLinkButtons();
                    break;
            }
            this.AddPreviousNextLinkButtons();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.CalculateTotalPages() > 1)
            {
                this.AddAttributesToRender(writer);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass, false);
                switch (this.RenderMode)
                {
                    case PagerRenderMode.Numeric:
                        this.RenderNumericMode(writer);
                        return;

                    case PagerRenderMode.PrevNext:
                        this.RenderPrevNextMode(writer);
                        return;

                    case PagerRenderMode.DropDownList:
                        this.RenderDropDownListMode(writer);
                        return;
                }
            }
        }
        #endregion

        #region Event methods
        private void PageIndex_Click(object sender, EventArgs e)
        {
            this.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            if (this.IndexChanged != null)
            {
                this.IndexChanged(sender, e);
            }
            if (this.PagerIndexChange != null)
            {
                this.PagerIndexChange(sender, new PagerEventArgs(this.PageSize, this.PageIndex, this.TotalRecords));
            }
        }

        private void PagingDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageIndex = ((DropDownList)sender).SelectedIndex;
            if (this.IndexChanged != null)
            {
                this.IndexChanged(sender, e);
            }
            if (this.PagerIndexChange != null)
            {
                this.PagerIndexChange(sender, new PagerEventArgs(this.PageSize, this.PageIndex, this.TotalRecords));
            }
        }
        #endregion
        // Nested Types: Pager mode
        public enum PagerRenderMode
        {
            Numeric,
            PrevNext,
            DropDownList
        }
    }


}
