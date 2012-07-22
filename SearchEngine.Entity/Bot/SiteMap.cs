using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Bot
{
    public class SiteMap:BaseObject
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Site Site { get; set; }
        public int Priority { get; set; }
    }
}
