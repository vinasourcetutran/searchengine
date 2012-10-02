using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Entity.Html
{
    public class HtmlPage
    {
        public Guid HtmlPageId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string Domain { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public virtual ICollection<HtmlMeta> MetaTags { get; set; }
        public virtual ICollection<HtmlUrl> Urls { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public HtmlPage()
        {
            this.HtmlPageId = Guid.NewGuid();
            this.CreatedDate = this.LastModificationDate = DateTime.UtcNow;
        }

    }
}
