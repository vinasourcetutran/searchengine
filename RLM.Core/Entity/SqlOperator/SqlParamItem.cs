using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlParamItem
    {
        #region Constructor
        public SqlParamItem() { }
        public SqlParamItem(string name, string value, bool isXmlPro)
        {
            this.Name = name;
            this.Value = value;
            this.IsXmlProperties = isXmlPro;
        }
        public SqlParamItem(string name, string value)
        {
            //SqlParamItem(name, value, false);
        }
        #endregion
        #region Properties
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsXmlProperties { get; set; }
        #endregion

    }
}
