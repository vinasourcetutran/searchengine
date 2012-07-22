using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace RLM.Core.Web.UI
{
    [ParseChildren(true)]
    public abstract class BasePager : Control, INamingContainer
    {
        // Fields
        public const int DEFAULT_MAX_DISPLAY_PAGES = 10;
        public const int DEFAULT_PAGE_SIZE = 20;
        private ITemplate descriptionTemplate;
        private bool isDatabound;

        // Methods
        protected BasePager()
        {
        }

        private int CalculateTotalPages()
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

        private void CalculateVisiblePageRange(out int fromPage, out int toPage)
        {
            int num = this.CalculateTotalPages();
            if (num <= this.MaxDisplayPages)
            {
                fromPage = 0;
                toPage = num;
            }
            else
            {
                fromPage = this.PageIndex - (this.MaxDisplayPages / 2);
                toPage = (this.PageIndex + (this.MaxDisplayPages / 2)) + (this.MaxDisplayPages % 2);
                if (fromPage <= 0)
                {
                    fromPage = 0;
                    toPage = fromPage + this.MaxDisplayPages;
                }
                if (toPage > num)
                {
                    toPage = num;
                    fromPage = num - this.MaxDisplayPages;
                }
            }
        }

        protected override void CreateChildControls()
        {
            int num;
            int num2;
            this.Controls.Clear();
            if (this.TotalRecords > 0)
            {
                if (this.descriptionTemplate != null)
                {
                    this.descriptionTemplate.InstantiateIn(this);
                }
                else
                {
                    Literal child = new Literal();
                    child.Text = string.Format(this.PagerDescriptionText, this.PageIndex + 1, this.TotalPages, this.TotalRecords);
                    this.Controls.Add(child);
                }
            }
            if (this.HasFirst)
            {
                this.Controls.Add(this.CreateFirstPaginatorControl());
            }
            this.Controls.Add(this.CreateSeparatorControl());
            if (this.HasPrevious)
            {
                this.Controls.Add(this.CreatePreviousPaginatorControl());
            }
            this.Controls.Add(this.CreateSeparatorControl());
            this.CalculateVisiblePageRange(out num, out num2);
            foreach (Control control in this.CreateMainPaginatorControls(num, num2))
            {
                this.Controls.Add(control);
                this.Controls.Add(this.CreateSeparatorControl());
            }
            if (this.HasNext)
            {
                this.Controls.Add(this.CreateNextPaginatorControl());
            }
            this.Controls.Add(this.CreateSeparatorControl());
            if (this.HasLast)
            {
                this.Controls.Add(this.CreateLastPaginatorControl());
            }
        }

        protected abstract Control CreateFirstPaginatorControl();
        protected abstract Control CreateLastPaginatorControl();
        protected abstract Control[] CreateMainPaginatorControls(int fromPage, int toPage);
        protected abstract Control CreateNextPaginatorControl();
        protected abstract Control CreatePreviousPaginatorControl();
        protected Control CreateSeparatorControl()
        {
            Literal literal = new Literal();
            literal.Text = " ";
            return literal;
        }

        public override void DataBind()
        {
            if (!base.ChildControlsCreated)
            {
                this.CreateChildControls();
            }
            base.ChildControlsCreated = true;
            this.isDatabound = true;
            base.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.isDatabound)
            {
                this.DataBind();
            }
            if (this.TotalRecords > this.PageSize)
            {
                base.Render(writer);
            }
        }

        // Properties
        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), TemplateContainer(typeof(BasePager)), Browsable(false)]
        public ITemplate DescriptionTemplate
        {
            get
            {
                return this.descriptionTemplate;
            }
            set
            {
                this.descriptionTemplate = value;
            }
        }

        public string FirstButtonText
        {
            get
            {
                if (this.ViewState["FirstButtonText"] == null)
                {
                    return "|<<";
                }
                return (string)this.ViewState["FirstButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["FirstButtonText"] = "|<<";
                }
                else
                {
                    this.ViewState["FirstButtonText"] = value;
                }
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

        private bool HasFirst
        {
            get
            {
                return ((this.PageIndex > (this.MaxDisplayPages / 2)) && (this.TotalPages > this.MaxDisplayPages));
            }
        }

        private bool HasLast
        {
            get
            {
                return ((((this.PageIndex + (this.MaxDisplayPages / 2)) + (this.MaxDisplayPages % 2)) < this.TotalPages) && (this.TotalPages > this.MaxDisplayPages));
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
        public string PagerDescriptionText
        {
            get
            {
                if (this.ViewState["PagerDescriptionText"] == null)
                {
                    return "Page {0} of {1} ({2} item(s))";
                }
                return (string)this.ViewState["PagerDescriptionText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["PagerDescriptionText"] = "Page {0} of {1} ({2} item(s))";
                }
                else
                {
                    this.ViewState["PagerDescriptionText"] = value;
                }
            }
        }
        public string LastButtonText
        {
            get
            {
                if (this.ViewState["LastButtonText"] == null)
                {
                    return ">>|";
                }
                return (string)this.ViewState["LastButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["LastButtonText"] = ">>|";
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
                    return ">";
                }
                return (string)this.ViewState["NextButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["NextButtonText"] = ">";
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

        [Bindable(true)]
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
                    return "<";
                }
                return (string)this.ViewState["PreviousButtonText"];
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.ViewState["PreviousButtonText"] = "<";
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
    }

 

}
