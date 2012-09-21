using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using System.Data;
using RLM.Core.Framework.Data;
using RLM.Core.Framework.Utility;

namespace SearchEngine.WindowService
{
    public class BackgroundServiceQueue
    {
        #region Variables
        
        #endregion

        #region Constructor
        static BackgroundServiceQueue()
        {
            Entity = new QueueIEntity();
        }
        #endregion

        #region Properties
        public static QueueIEntity Entity { get; set; }
        public static IDictionary<string, IDataReader<BaseEntity, string>> DataReader { get; set; }
        public static IDictionary<string, IDataWriter<BaseEntity, string>> DataWriter { get; set; }
        #endregion

        #region Public methods
        public static void InitDataReaderAndWriter()
        {
            //var forceLoad = typeof(BaseDataReader<BaseEntity, string>);
            if (DataReader == null) { DataReader = new Dictionary<string, IDataReader<BaseEntity, string>>(5); }
            if (DataWriter == null) { DataWriter = new Dictionary<string, IDataWriter<BaseEntity, string>>(5); }

            DataAccessConfigs configs = XmlHelper.Deserialize<DataAccessConfigs>(Configuration.Configuration.GetInstance().BackgroundService.DataAccessConfigFile);
            foreach(DataAccessConfigItem item in configs)
            {
                
                if (!item.Enable) { continue; }

                IConfigurable config = null;
                if (item.Param != null)
                {
                    config = ClassHelper.CreateInstance<IConfigurable>(item.Param.InstanceClassName);
                    config.FromString(item.Param.Value);
                }
                if(item.IsReader)
                {

                    IDataReader<BaseEntity, string> reader = Activator.CreateInstance(Type.GetType(item.DataReader), config) as IDataReader<BaseEntity, string>;
                    DataReader[item.EntityClassName]= reader;
                }

                if(item.IsWriter)
                {
                    IDataWriter<BaseEntity, string> writer = (IDataWriter<BaseEntity, string>)Activator.CreateInstance(Type.GetType(item.DataWriter), config);
                    DataWriter[item.EntityClassName]= writer;
                }
            }

        }
        #endregion 

        #region Private methods
        #endregion

        
    }
}
