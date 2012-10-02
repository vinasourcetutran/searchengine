using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SearchEngine.Entity.Html;
using SearchEngine.Entity.Test;

namespace SearchEngine.Data
{
    public class SearchEngineContext:DbContext
    {
        public DbSet<TestDatabaseWriterQueueItem> TestDatabaseWriterQueueItem { get; set; }
        public DbSet<HtmlPage> HtmlPage { get; set; }
        public DbSet<HtmlMeta> HtmlMeta { get; set; }
        public DbSet<HtmlUrl> HtmlUrl { get; set; }
        public DbSet<T> DbSet<T>() where T:class
        {
            return SearchEngineContext.Context.Set<T>();
        }

        public static SearchEngineContext Context { get; set; }

        static SearchEngineContext()
        {
            if (SearchEngineContext.Context == null)
            {
                SearchEngineContext.Context = new SearchEngineContext();
            }
        }

    }
}
