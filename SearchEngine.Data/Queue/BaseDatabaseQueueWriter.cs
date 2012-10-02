using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data;
using RLM.Core.Entity;
using SearchEngine.Entity;
using System.Data.Entity;

namespace SearchEngine.Data.Queue
{
    public class BaseDatabaseQueueWriter<EntityType>:BaseDataWriter<EntityType> where EntityType:class, IEntity
    {
        #region Variable
        IConfigurable config;
        object getObj = new object();
        Queue<EntityType> queue;
        DbSet<EntityType> dbSet;
        #endregion

        #region Constructor
        public BaseDatabaseQueueWriter(IConfigurable config):base(config)
        {
            this.config = config;
            Init();
        }
        ~BaseDatabaseQueueWriter()
        {
        }
        #endregion

        #region Properties
        public int MaxQueueSize { get; set; }
        #endregion

        #region Implement interface
        public override void InsertOrUpdate(IList<EntityType> items)
        {
            foreach (EntityType item in items)
            {
                dbSet.Add(item);
            }
            SearchEngineContext.Context.SaveChanges();
        }

        public override void InsertOrUpdate(EntityType item)
        {
            this.queue.Enqueue(item);
            if (this.queue.Count > this.MaxQueueSize)
            {
                while (this.queue.Count>0)
                {
                    dbSet.Add(this.queue.Dequeue());
                }
                SearchEngineContext.Context.SaveChanges();
            }
        }

        #endregion

        #region Private methods
        private void Init()
        {
            int maxQueueSize;
            int.TryParse(config.GetKey(ConfigFieldDatabaseQueue.MaxQueueSize.ToString(), "0").ToString(), out maxQueueSize);
            this.MaxQueueSize = maxQueueSize;
            this.queue = new Queue<EntityType>();
            this.dbSet = SearchEngineContext.Context.DbSet<EntityType>();
        }
        #endregion
    }
}
