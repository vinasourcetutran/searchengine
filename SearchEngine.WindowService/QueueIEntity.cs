using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using SearchEngine.Bot.Entity;
using RLM.Core.Framework.Log;

namespace SearchEngine.WindowService
{
    public class QueueIEntity
    {
        #region Variables
        Queue<IEntity> entity;
        #endregion

        #region Properties
        public int ItemPerPage { get; set; }
        #endregion

        #region Contruction
        public QueueIEntity()
        {
            this.entity = new Queue<IEntity>();
            this.ItemPerPage = 10;
            LoadDataIntoQueue();
        }
        public QueueIEntity(int itemperPage)
        {
            this.entity = new Queue<IEntity>();
            this.ItemPerPage = itemperPage;
        }
        #endregion

        #region Public methods
        public void Enqueue(IEntity item)
        {
            if (entity == null)
            {
                entity = new Queue<IEntity>();
            }
            entity.Enqueue(item);
        }

        public IEntity Dequeue()
        {
            if (entity == null || entity.Count == 0)
            {
                int numberOfRecord = LoadDataIntoQueue();
                if (numberOfRecord == 0) { return null; }
            }
            return entity.Dequeue();
        }

        public virtual int LoadDataIntoQueue()
        {
            Logger.Info("Load data into queue...");
            Enqueue(new TestEntity() { Name="Entity 1:"+ DateTime.Now });
            Enqueue(new TestEntity() { Name = "Entity 2:" + DateTime.Now });
            Enqueue(new TestEntity() { Name = "Entity 3:" + DateTime.Now });
            Enqueue(new TestEntity() { Name = "Entity 4:" + DateTime.Now });
            Enqueue(new TestEntity() { Name = "Entity 5:" + DateTime.Now });
            /// TO DO: Load data into queue
            return 5;
        }
        #endregion

        #region Private methods
        #endregion
    }
}
