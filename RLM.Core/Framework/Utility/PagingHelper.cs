using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class PagingHelper
    {
        public static string[] GetPage(string[] items, int pageIndex, int pageSize, out int totalPages)
        {
            totalPages = 0;
            if (pageIndex == 0 && pageSize == 0) { return items ?? new string[0]; }
            if (items == null || items.Length == 0 || items.Length < pageSize * pageIndex) { return new string[0]; }
            if (pageSize == 0)
            {
                pageIndex = 0; pageSize = items.Length;
            }
            totalPages = items.Length / pageSize + items.Length % pageSize > 0 ? 1 : 0;
            int from = pageSize * pageIndex;
            int to = (pageIndex + 1) * pageSize;

            to = to > items.Length ? items.Length : to;
            string[] result = new string[to - from];
            for(int index=from; index<to; index++){
                result[index - from] = items[index];
            }
            return result;
        }
    }
}
