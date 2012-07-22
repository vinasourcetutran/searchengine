using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEngine.Bot.Entity;

namespace SearchEngine.Entity.Bot
{
    public class HtmlPage : BaseObject
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string RelativeUrl { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Language Language { get; set; }
        public string RawHtml { get; set; }
        public string Text { get; set; }
    }
}
