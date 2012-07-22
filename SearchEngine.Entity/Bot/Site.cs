using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Bot
{
    public class Site : BaseObject
    {
        public Guid ParentSiteId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public string Summary { get; set; }
        public Country Country { get; set; }
        public string ServerIP { get; set; }
        public IList<SitePage> SitePages { get; set; }
        public SiteMap SiteMap { get; set; }
        public IList<SiteCategory> SiteCategories { get; set; }
        public bool IsSubsite { get; set; }
        public bool Enable { get; set; }
        public int Priority { get; set; }
        public DateTime LastCrawlerTime { get; set; }
        public DateTime NextCrawlerTime { get; set; }
    }
}
