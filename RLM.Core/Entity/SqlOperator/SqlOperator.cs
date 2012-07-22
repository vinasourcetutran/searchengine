using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlOperator:SqlParamItem
    {
        #region Properties
        public bool IsNeedQuote { get; set; }
        #endregion
        #region Constructor
        public SqlOperator(string name, string value, bool isXmlPro):base(name,value,isXmlPro)
        {
            this.IsNeedQuote = true;
        }
        public SqlOperator(): base()
        {
            this.IsNeedQuote = true;
        }
        #endregion
        #region Methods
        public virtual string ToSql()
        {
            throw new Exception("this funcntion was not implemented soon");
        }
        #endregion
    }
}
