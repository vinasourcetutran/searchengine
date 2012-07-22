using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;

namespace RLM.Core.Entity.SqlOperator
{
    public class SqlParameter:List<SqlOperator>
    {
        #region methods
        public void Add(string name, string value, bool isXmlPro)
        {
            SqlOperator item = new SqlOperator(name, value, isXmlPro);
            this.Add(item);
        }

        public string ToSqlUsingAnd()
        {
            return ToSql(SqlLogicOperatorEnum.AND);
        }
        public string ToSqlUsingOr()
        {
            return ToSql(SqlLogicOperatorEnum.OR);
        }
        public string ToSql(SqlLogicOperatorEnum oper)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                int count = 0;
                foreach (SqlOperator item in this)
                {
                    sql.Append(item.ToSql());
                    if (count++ < this.Count-1)
                    {
                        sql.Append(oper == SqlLogicOperatorEnum.OR ? " OR  " : " AND ");
                    }
                }
                return sql.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }
        #endregion

    }
}
