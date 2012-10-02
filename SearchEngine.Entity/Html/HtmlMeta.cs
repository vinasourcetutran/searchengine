using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Entity.Html
{
    public class HtmlMeta
    {
        public Guid HtmlMetaID { get; set; }
        public Guid HtmlPageID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }

        #region Constructor
        public HtmlMeta()
        {
            this.HtmlMetaID = Guid.NewGuid();
            this.CreatedDate = DateTime.UtcNow;
        }
        #endregion
    }
}
