using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace RLM.Core.Web.UI
{
    public class Repeater : System.Web.UI.WebControls.Repeater
    {

        #region Attributes
        // on none item data bound event
        private static readonly object EventNoneItemsDataBound = new object();
        // none data bound template
        private ITemplate noneTemplate;
        #endregion

        #region Properties
        // event was fired when none data was bound
        public event RepeaterItemEventHandler NoneItemsDataBound
        {
            add
            {
                base.Events.AddHandler(EventNoneItemsDataBound, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventNoneItemsDataBound, value);
            }
        }
        // template that will be used when none data was bounded
        [Browsable(false), DefaultValue((string)null), Description("Defines the ITemplate to be used when no items are defined in the datasource."), PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ITemplate NoneTemplate
        {
            get
            {
                return this.noneTemplate;
            }
            set
            {
                this.noneTemplate = value;
            }
        }
        // whether show header and footer when none data
        [DefaultValue(true)]
        public virtual bool ShowHeaderFooterOnNone
        {
            get
            {
                object obj2 = this.ViewState["ShowHeaderFooterOnNone"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowHeaderFooterOnNone"] = value;
            }
        }
        #endregion

        #region Methods
        protected override void CreateControlHierarchy(bool useDataSource)
        {
            base.CreateControlHierarchy(useDataSource);
            if ((this.Items.Count == 0) && (this.NoneTemplate != null))
            {
                this.Controls.Clear();
                if (this.ShowHeaderFooterOnNone && (this.HeaderTemplate != null))
                {
                    RepeaterItem item = this.CreateItem(-1, ListItemType.Header);
                    RepeaterItemEventArgs args = new RepeaterItemEventArgs(item);
                    this.InitializeItem(item);
                    this.OnItemCreated(args);
                    this.Controls.Add(item);
                    item.DataBind();
                    this.OnItemDataBound(args);
                }
                RepeaterItem item2 = new RepeaterItem(-1, ListItemType.Item);
                RepeaterItemEventArgs e = new RepeaterItemEventArgs(item2);
                this.NoneTemplate.InstantiateIn(item2);
                this.OnItemCreated(e);
                this.Controls.Add(item2);
                this.OnNoneItemsDataBound(e);
                if (this.ShowHeaderFooterOnNone && (this.FooterTemplate != null))
                {
                    RepeaterItem item3 = this.CreateItem(-1, ListItemType.Footer);
                    RepeaterItemEventArgs args3 = new RepeaterItemEventArgs(item3);
                    this.InitializeItem(item3);
                    this.OnItemCreated(args3);
                    this.Controls.Add(item3);
                    item3.DataBind();
                    this.OnItemDataBound(args3);
                }
                base.ChildControlsCreated = true;
            }
        }
        protected virtual void OnNoneItemsDataBound(RepeaterItemEventArgs e)
        {
            RepeaterItemEventHandler handler = (RepeaterItemEventHandler)base.Events[EventNoneItemsDataBound];
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
