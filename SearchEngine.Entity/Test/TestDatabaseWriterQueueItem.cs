using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.Entity.Test
{
    public class TestDatabaseWriterQueueItem : IEntity, IQueueable<string>
    {
        public int TestDatabaseWriterQueueItemId { get; set; }
        public string Name { get; set; }
        public string EntityName
        {
            get { return Name; }
        }

        public string EntityType
        {
            get { return this.GetType().FullName; }
        }

        public string EntityId {get;set;}

        public int Status { get; set; }

        public DateTime NextRun { get; set; }

        public int Priority { get; set; }

        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }
    }
}
