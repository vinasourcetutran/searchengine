using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Bot
{
    public class SiteCategory:BaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public int Priority { get; set; }
        public SiteCategory Parent { get; set; }
        public string IconUrl { get; set; }
    }
}
