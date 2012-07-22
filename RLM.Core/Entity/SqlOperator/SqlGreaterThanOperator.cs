using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlGreaterThanOperator:SqlOperator
    {
        public override string ToSql()
        {
            return string.Format(
                this.IsNeedQuote ? "{0}>'{1}'" : "{0}>{1}",
                base.Name,
                base.Value
            );
        }
    }
}
