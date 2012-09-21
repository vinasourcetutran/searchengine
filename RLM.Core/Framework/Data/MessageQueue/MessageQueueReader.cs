using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data.MessageQueue
{
    public class MessageQueueReader<EntityType>:BaseDataReader<EntityType, string> where EntityType: BaseEntity, new()
    {
        #region Variable
        IConfigurable config;
        System.Messaging.MessageQueue messageQueue;
        object getObj = new object();
        Queue<QueueEntity> queue;
        #endregion

        #region Constructor
        public MessageQueueReader(IConfigurable config):base(config)
        {
            this.config = config;
            Init();
        }
        ~MessageQueueReader()
        {
            this.messageQueue.Dispose();
        }
        #endregion

        #region Properties
        public string QueuePath { get; set; }
        public int MaxQueueSize { get; set; }
        private bool IsWaitingForMessage { get; set; }
        #endregion

        #region Implement interface
        public override IList<EntityType> GetList(int pagesize, int pageIndex, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override EntityType Get()
        {
            if (this.queue.Count <= 0) { return default(EntityType); }
            lock (getObj)
            {
                QueueEntity entity = this.queue.Dequeue();
                if (this.queue.Count < this.MaxQueueSize && !this.IsWaitingForMessage)
                {
                    this.messageQueue.BeginReceive();
                    this.IsWaitingForMessage = true;
                }
                return entity.GetData<EntityType>();
            }
            
        }

        public override EntityType Get(string id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        private void Init()
        {
            this.QueuePath = config.GetKey(ConfigFieldMessageQueue.QueuePath.ToString()) as string;

            int maxQueueSize;
            int.TryParse(config.GetKey(ConfigFieldMessageQueue.MaxQueueSize.ToString(),"").ToString(), out maxQueueSize);
            this.MaxQueueSize = maxQueueSize;

            this.queue = new Queue<QueueEntity>();

            this.messageQueue = new System.Messaging.MessageQueue(this.QueuePath);
            this.messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(QueueEntity) });
            this.messageQueue.ReceiveCompleted  +=new ReceiveCompletedEventHandler(messageQueue_ReceiveCompleted);
            this.messageQueue.BeginReceive();
            this.IsWaitingForMessage = true;
        }
        private void messageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            this.IsWaitingForMessage = false;
            QueueEntity entity = (QueueEntity)e.Message.Body;
            this.queue.Enqueue(entity);
            if (this.queue.Count < this.MaxQueueSize && !this.IsWaitingForMessage)
            {
                this.messageQueue.BeginReceive();
                this.IsWaitingForMessage = true;
            }
        }
        #endregion

        
    }
}
