using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data.MessageQueue
{

    public class MessageQueueWriter<EntityType> : BaseDataWriter<EntityType, string>
    {
        #region Variable
        IConfigurable config;
        System.Messaging.MessageQueue messageQueue;
        object getObj = new object();
        Queue<QueueEntity> queue;
        #endregion

        #region Constructor
        public MessageQueueWriter(IConfigurable config):base(config)
        {
            this.config = config;
            Init();
        }
        ~MessageQueueWriter()
        {
            this.messageQueue.Dispose();
        }
        #endregion

        #region Properties
        public string QueuePath { get; set; }
        public bool IsTransactional { get; set; }
        public bool IsAutoCreateQueueIfNotExist { get; set; }
        #endregion

        #region Implement interface
        public override void InsertOrUpdate(IList<EntityType> items)
        {
            MessageQueueTransaction msgTran = null;
            if (this.IsTransactional)
            {
                msgTran = new MessageQueueTransaction();
                msgTran.Begin();
            }
            foreach (EntityType item in items)
            {
                InsertOrUpdateWithoutTransaction(item, msgTran);
            }
            if (this.IsTransactional)
            {
                msgTran.Commit();
            }
        }

        public override void InsertOrUpdate(EntityType item)
        {
            MessageQueueTransaction msgTran = null;
            if (this.IsTransactional)
            {
                msgTran = new MessageQueueTransaction();
                msgTran.Begin();
            }
            InsertOrUpdateWithoutTransaction(item, msgTran);
            if (this.IsTransactional)
            {
                msgTran.Commit();
            }
        }

        public void InsertOrUpdateWithoutTransaction(EntityType item, MessageQueueTransaction transaction)
        {
            System.Messaging.Message msg = new System.Messaging.Message();

            QueueEntity dataItem = new QueueEntity();
            dataItem.SetData<EntityType>(item);
            msg.Body = dataItem;


            Type type = typeof(EntityType);
            msg.Label = string.Format("{0}.{1}", type.Namespace, type.Name);
            if (transaction != null)
            {
                this.messageQueue.Send(msg, transaction);
            }
            else
            {
                this.messageQueue.Send(msg);
            }
        }

        public override bool Delete(string id)
        {
            return true;
        }

        #endregion

        #region Private methods
        private void Init()
        {
            this.QueuePath = config.GetKey(ConfigFieldMessageQueue.QueuePath.ToString()) as string;
            this.IsTransactional = (bool)config.GetKey(ConfigFieldMessageQueue.IsTransactional.ToString());
            this.IsAutoCreateQueueIfNotExist = (bool)config.GetKey(ConfigFieldMessageQueue.IsAutoCreateQueueIfNotExist.ToString());

            if (!System.Messaging.MessageQueue.Exists(this.QueuePath) && this.IsAutoCreateQueueIfNotExist)
            {
                System.Messaging.MessageQueue.Create(this.QueuePath, this.IsTransactional);
            }

            this.messageQueue = new System.Messaging.MessageQueue(this.QueuePath);
            this.messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(QueueEntity) });
        }
        #endregion

    }
}
