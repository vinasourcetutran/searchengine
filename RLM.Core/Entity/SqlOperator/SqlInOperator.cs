using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlInOperator:SqlOperator
    {
        public override string ToSql()
        {
            return string.Format(
                "{0} in ({1})",
                base.Name,
                base.Value
            );
        }
    }
}
