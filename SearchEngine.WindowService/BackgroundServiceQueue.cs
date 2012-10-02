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
        public static IDictionary<string, IDataReader<BaseEntityObject>> DataReader { get; set; }
        public static IDictionary<string, IDataWriter<BaseEntityObject>> DataWriter { get; set; }
        #endregion

        #region Public methods
        public static void InitDataReaderAndWriter()
        {
            //var forceLoad = typeof(BaseDataReader<BaseEntity, string>);
            if (DataReader == null) { DataReader = new Dictionary<string, IDataReader<BaseEntityObject>>(5); }
            if (DataWriter == null) { DataWriter = new Dictionary<string, IDataWriter<BaseEntityObject>>(5); }

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

                    IDataReader<BaseEntityObject> reader = Activator.CreateInstance(Type.GetType(item.DataReader), config) as IDataReader<BaseEntityObject>;
                    DataReader[item.Key]= reader;
                }

                if(item.IsWriter)
                {
                    IDataWriter<BaseEntityObject> writer = (IDataWriter<BaseEntityObject>)Activator.CreateInstance(Type.GetType(item.DataWriter), config);
                    DataWriter[item.Key]= writer;
                }
            }

        }
        #endregion 

        #region Private methods
        #endregion

        
    }
}
