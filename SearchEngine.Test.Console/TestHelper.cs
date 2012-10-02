using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data.Config;
using RLM.Core.Framework.Data;
using RLM.Core.Framework.Data.MessageQueue;
using SearchEngine.Bot.Entity;
using System.Threading;
using RLM.Core.Entity;
using RLM.Core.Framework.Utility;
using System.Xml.Serialization;
using System.Xml;
using SearchEngine.Data;
using SearchEngine.Entity.Html;
using SearchEngine.Entity;
using SearchEngine.Data.Queue;
using SearchEngine.Entity.Test;

namespace SearchEngine.Test.ConsoleApp
{
    public class TestHelper
    {
        public static void TestDatabaseWriterQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldDatabaseQueue.MaxQueueSize.ToString(), 3);

            IDataWriter<TestDatabaseWriterQueueItem> writer = new BaseDatabaseQueueWriter<TestDatabaseWriterQueueItem>(config);

            //Thread th = new Thread(ReadFromQueue);
            //th.Start();
            string text = "";
            while (text != "exit")
            {
                text = Console.ReadLine();
                TestDatabaseWriterQueueItem entity = new TestDatabaseWriterQueueItem();
                entity.Name = text;
                entity.NextRun = DateTime.UtcNow;
                entity.Status = (int)StatusCode.Fresh;
                writer.InsertOrUpdate(entity);
            }
        }

        public static void TestDatabaseReaderQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldDatabaseQueue.MaxQueueSize.ToString(), 3);

            IDataReader<TestDatabaseWriterQueueItem> reader = new BaseDatabaseQueueReader<TestDatabaseWriterQueueItem,string>(config);

            //Thread th = new Thread(ReadFromQueue);
            //th.Start();
            TestDatabaseWriterQueueItem item = reader.Get();
            while (item!=null)
            {
                Console.WriteLine(item.EntityName);
                item = reader.Get();
            }
        }
        public static void TestMessageQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldMessageQueue.IsAutoCreateQueueIfNotExist.ToString(),true);
            config.Add(ConfigFieldMessageQueue.IsTransactional.ToString(), true);
            config.Add(ConfigFieldMessageQueue.QueuePath.ToString(), @".\private$\receiver1");
            config.Add(ConfigFieldMessageQueue.MaxQueueSize.ToString(), 10);

            IDataWriter<BaseEntityObject> writer = new MessageQueueWriter<BaseEntityObject>(config);

            //Thread th = new Thread(ReadFromQueue);
            //th.Start();
            string text="";
            while (text!="exit")
            {
                text = Console.ReadLine();
                TestEntity entity = new TestEntity() { Name = text};
                BaseEntityObject queEntity = new BaseEntityObject();
                queEntity.SetData<TestEntity>(entity);
                writer.InsertOrUpdate(queEntity);
            }
        }

        public static void ReadFromQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldMessageQueue.IsAutoCreateQueueIfNotExist.ToString(), true);
            config.Add(ConfigFieldMessageQueue.IsTransactional.ToString(), true);
            config.Add(ConfigFieldMessageQueue.QueuePath.ToString(), @".\private$\receiver1");
            config.Add(ConfigFieldMessageQueue.MaxQueueSize.ToString(), 10);

            IDataReader<BaseEntityObject> reader = new MessageQueueReader<BaseEntityObject>(config);

            while (true)
            {
                BaseEntityObject entity = reader.Get();
                if (entity == null) { continue; }
                TestEntity test = entity.GetData<TestEntity>();
                Console.WriteLine("Get from queue:{0}/{1}",test.Name, test.EntityId);
                Thread.Sleep(2000);
            }
        }

        internal static void TestDataConfig()
        {
            DataAccessConfigs config = XmlHelper.Deserialize<DataAccessConfigs>(@"D:\Project\SearchEngine\trunk\SearchEngine.Test.Console\DataAccess1.xml");
            foreach (DataAccessConfigItem item in config)
            {
                Console.WriteLine("Entity name:{0}, Data reader:{1}, Data writer:{2}, Is reader:{3}, is writer:{4}, param class name:{5}, param value:{6}",
                    item.Key,
                    item.DataReader,
                    item.DataWriter,
                    item.IsWriter,
                    item.IsReader,
                    item.Param!=null?item.Param.InstanceClassName:"",
                    item.Param!=null?item.Param.Value:""
                    );
            }
        }

        public static void TestSerializeDataConfig()
        {
            DataAccessConfigs config = new DataAccessConfigs();
            config.Add(new DataAccessConfigItem() {
                Key = "EntityClassName",
                DataWriter = "DataWriter",
                DataReader = "DataReader",
                IsReader=true,
                IsWriter=true
            });

            string path = @"D:\Project\SearchEngine\trunk\SearchEngine.Test.Console\DataAccess1.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(DataAccessConfigs));

            // A FileStream is needed to read the XML document.
            using (XmlWriter reader = XmlWriter.Create(path))
            {
                serializer.Serialize(reader, config);
            }

        }

        public static void TestSerializeWorkflow()
        {

            string path = @"D:\Project\SearchEngine\trunk\SearchEngine.Test.Console\Workflow.xml";
            BackgroundServiceWorkflows workflows = new BackgroundServiceWorkflows();
            workflows.Add(new BackgroundServiceWorkflow() { Key="abc",MaxThread=10,Enable=true,Workflow="edf"});

            XmlSerializer serializer = new XmlSerializer(typeof(BackgroundServiceWorkflows));

            // A FileStream is needed to read the XML document.
            using (XmlWriter reader = XmlWriter.Create(path))
            {
                serializer.Serialize(reader, workflows);
            }
        }

        internal static void TestWorkflow()
        {
            BackgroundServiceWorkflows config = XmlHelper.Deserialize<BackgroundServiceWorkflows>(@"D:\Project\SearchEngine\trunk\SearchEngine.Test.Console\Workflow.xml");
            foreach (BackgroundServiceWorkflow item in config)
            {
                Console.WriteLine("Entity name:{0}, Data reader:{1}, Data writer:{2}, Is reader:{3}",
                    item.Key,
                    item.Enable,
                    item.MaxThread,
                    item.Workflow
                    );
            }
        }

        internal static void TestDataReader()
        {
            string dataReader = "";
            MessageQueueReader<TestEntity> reader = new MessageQueueReader<SearchEngine.Bot.Entity.TestEntity>(new ConfigHash());
            IDataReader<BaseEntity> t = reader as IDataReader<BaseEntity>;
        }

        internal static void CreateDataReader()
        {

            IConfigurable config = null;
            config = ClassHelper.CreateInstance<IConfigurable>("RLM.Core.Framework.Data.Config.ConfigHash");
                config.FromString(@"IsAutoCreateQueueIfNotExist::True;;IsTransactional::True;;QueuePath::.\private$\receiver1;;MaxQueueSize::10");

                Type type=Type.ReflectionOnlyGetType("SearchEngine.Bot.Entity.TestEntity",false, true);


                IDataReader<BaseEntity> reader = Activator.CreateInstance(Type.GetType("SearchEngine.WindowService.Workflow.ConsoleReader,SearchEngine.WindowService"), config) as IDataReader<BaseEntity>;
                Console.WriteLine("Init data reader:{0}", reader==null?"Null":"Not null");
        }

        internal static void AddNewHtmlPage()
        {
            HtmlPage page = new HtmlPage();
            page.Title = "test page";

            SearchEngineContext.Context.HtmlPage.Add(page);
            SearchEngineContext.Context.SaveChanges();
        }
    }
}
