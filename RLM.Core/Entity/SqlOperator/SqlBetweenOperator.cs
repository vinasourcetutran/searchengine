using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlBetweenOperator:SqlOperator
    {
        #region Properties
        public string SecondValue { get; set; }
        #endregion
        #region Constructor
        public SqlBetweenOperator(string name, string value, string secondValue, bool isXmlPro)
            : base(name, value, isXmlPro)
        {
            this.SecondValue = secondValue;
        }
        public SqlBetweenOperator() : base() { }
        #endregion
        #region Methods
        public override string ToSql()
        {
            return string.Format(
                this.IsNeedQuote ? "{0} between '{1}' and '{2}'" : "{0} between {1} and {2}",
                base.Name,
                base.Value
            );
        }
        #endregion
    }
}
