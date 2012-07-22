using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEngine.Entity.Bot;

namespace SearchEngine.Bot.Entity.Bot
{
    public class SitePage : BaseObject
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public HtmlPage HtmlPage { get; set; }
    }
}
