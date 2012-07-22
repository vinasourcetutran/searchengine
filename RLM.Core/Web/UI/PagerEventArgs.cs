using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Web.UI
{
    public class PagerEventArgs : EventArgs
    {
        // Fields
        private int pageIndex;
        private int pageSize;
        private int totalRecords;

        // Methods
        public PagerEventArgs(int pageSize, int pageIndex, int totalRecords)
        {
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
            this.totalRecords = totalRecords;
        }

        // Properties
        public int PageIndex
        {
            get
            {
                return this.pageIndex;
            }
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        public int TotalRecords
        {
            get
            {
                return this.totalRecords;
            }
        }
    }


}
