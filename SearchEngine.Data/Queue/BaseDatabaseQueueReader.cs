using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data;
using RLM.Core.Entity;
using System.Timers;
using System.Data.Entity;
using SearchEngine.Entity;

namespace SearchEngine.Data.Queue
{
    public class BaseDatabaseQueueReader<EntityType, PKType> : BaseDataReader<EntityType> where EntityType : class,IEntity, IQueueable<PKType>
    {
        #region Variable
        IConfigurable config;
        object getObj = new object();
        Queue<EntityType> queue;
        Timer timer;
        #endregion

        #region Constructor
        public BaseDatabaseQueueReader(IConfigurable config):base(config)
        {
            this.config = config;
            Init();
        }
        ~BaseDatabaseQueueReader()
        {
            this.timer.Dispose();
        }
        #endregion

        #region Properties
        public int MaxQueueSize { get; set; }
        #endregion

        #region Implement interface
        public override IList<EntityType> GetList(int pagesize, int pageIndex, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override EntityType Get()
        {
            lock (getObj)
            {
                if (this.queue.Count <=0)
                {
                    LoadData();
                }
                if (this.queue.Count == 0) { return null; }
                return this.queue.Dequeue();
            }
        }

        

        public override EntityType Get(string id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        private void LoadData()
        {
            ///Get the list from database
            DbSet<EntityType> dbSet = SearchEngineContext.Context.DbSet<EntityType>();
            IList<EntityType> result = dbSet.Where(item => item.Status == (int)StatusCode.Fresh && item.NextRun <= DateTime.UtcNow)
                                        .OrderByDescending(item => item.Priority).OrderBy(item => item.NextRun)
                                        .Take(this.MaxQueueSize)
                                        .ToList();
            ///Update status to Queue
            foreach (EntityType item in result)
            {
                item.Status = (int)StatusCode.Queue;
                this.queue.Enqueue(item);
            }
            SearchEngineContext.Context.SaveChanges();
        }

        private void Init()
        {
            int maxQueueSize;
            int.TryParse(config.GetKey(ConfigFieldDatabaseQueue.MaxQueueSize.ToString(),"").ToString(), out maxQueueSize);
            this.MaxQueueSize = maxQueueSize;
            this.queue = new Queue<EntityType>();

            //this.timer = new Timer();
            //this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        //void timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        
        #endregion
    }
}
