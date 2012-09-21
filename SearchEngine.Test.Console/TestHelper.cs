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

namespace SearchEngine.Test.ConsoleApp
{
    public class TestHelper
    {
        public static void TestMessageQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldMessageQueue.IsAutoCreateQueueIfNotExist.ToString(),true);
            config.Add(ConfigFieldMessageQueue.IsTransactional.ToString(), true);
            config.Add(ConfigFieldMessageQueue.QueuePath.ToString(), @".\private$\receiver1");
            config.Add(ConfigFieldMessageQueue.MaxQueueSize.ToString(), 10);

            IDataWriter<TestEntity,string> writer = new MessageQueueWriter<TestEntity>(config);

            //Thread th = new Thread(ReadFromQueue);
            //th.Start();
            string text="";
            while (text!="exit")
            {
                text = Console.ReadLine();
                writer.InsertOrUpdate(new TestEntity() { Name=text+ DateTime.Now.ToString()});
            }
        }

        public static void ReadFromQueue()
        {
            IConfigurable config = new ConfigHash();
            config.Add(ConfigFieldMessageQueue.IsAutoCreateQueueIfNotExist.ToString(), true);
            config.Add(ConfigFieldMessageQueue.IsTransactional.ToString(), true);
            config.Add(ConfigFieldMessageQueue.QueuePath.ToString(), @".\private$\receiver1");
            config.Add(ConfigFieldMessageQueue.MaxQueueSize.ToString(), 10);

            IDataReader<TestEntity, string> reader = new MessageQueueReader<TestEntity>(config);

            while (true)
            {
                BaseEntity entity = reader.Get();
                Console.WriteLine("Get from queue:"+ (entity!=null?entity.EntityName: "Null"));
                Thread.Sleep(2000);
            }
        }

        internal static void TestDataConfig()
        {
            DataAccessConfigs config = XmlHelper.Deserialize<DataAccessConfigs>(@"D:\Project\SearchEngine\trunk\SearchEngine.Test.Console\DataAccess1.xml");
            foreach (DataAccessConfigItem item in config)
            {
                Console.WriteLine("Entity name:{0}, Data reader:{1}, Data writer:{2}, Is reader:{3}, is writer:{4}, param class name:{5}, param value:{6}",
                    item.EntityClassName,
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
                EntityClassName = "EntityClassName",
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
            workflows.Add(new BackgroundServiceWorkflow() { EntityClassName="abc",MaxThread=10,Enable=true,Workflow="edf"});

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
                    item.EntityClassName,
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
            IDataReader<BaseEntity, string> t = reader as IDataReader<BaseEntity, string>;
        }

        internal static void CreateDataReader()
        {

            IConfigurable config = null;
            config = ClassHelper.CreateInstance<IConfigurable>("RLM.Core.Framework.Data.Config.ConfigHash");
                config.FromString(@"IsAutoCreateQueueIfNotExist::True;;IsTransactional::True;;QueuePath::.\private$\receiver1;;MaxQueueSize::10");

                Type type=Type.ReflectionOnlyGetType("SearchEngine.Bot.Entity.TestEntity",false, true);


                IDataReader<BaseEntity, string> reader = Activator.CreateInstance(Type.GetType("SearchEngine.WindowService.Workflow.ConsoleReader,SearchEngine.WindowService"), config) as IDataReader<BaseEntity, string>;
                Console.WriteLine("Init data reader:{0}", reader==null?"Null":"Not null");
        }
    }
}
