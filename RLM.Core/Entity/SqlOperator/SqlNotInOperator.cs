using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlNotInOperator:SqlOperator
    {
        public override string ToSql()
        {
            return string.Format(
                "{0} not in ({1})",
                base.Name,
                base.Value
            );
        }
    }
}
