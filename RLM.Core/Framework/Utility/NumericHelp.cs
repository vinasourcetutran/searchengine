using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class NumericHelper
    {
        public static double GetPercent(decimal source, decimal des, bool isReverse)
        {
            double percent=(double)(source*100/des);
            return isReverse ?Math.Abs(100 - percent) : percent;
        }



        public static decimal GetDecimal(string strprice, decimal defaultValue)
        {
            decimal price;
            return decimal.TryParse(strprice, out price) ? price : defaultValue;
        }
    }
}
