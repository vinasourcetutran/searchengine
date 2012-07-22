using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public class BaseDropdownList : System.Web.UI.WebControls.DropDownList
    {
        #region Priperties
        public string SelectedValue { get; set; }
        public bool IsDisplayAll { get; set; }

        public virtual string DisplayAllText { get; set; }

        public virtual string DisplayAllValue { get; set; }

        public virtual string TextField
        {
            get { throw new Exception("TextField was not implemented yet"); }
        }

        public virtual string ValueField
        {
            get { throw new Exception("ValueField was not implemented yet"); }
        }

        public bool IsActiveOnly { get; set; }
        #endregion

        #region Override

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            try
            {
                this.Items.FindByValue(this.SelectedValue).Selected = true;
            }
            catch (Exception ex){}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Page.IsPostBack) { return; }
            this.DataTextField = this.TextField;
            this.DataValueField = this.ValueField;
            this.BindData();
        }
        #endregion

        #region Virtual
        public virtual void BindData()
        {
        }
        #endregion
    }
}
