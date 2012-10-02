using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RLM.Core.Entity
{
    [Serializable]
    public class BaseEntityObject:IEntity
    {
        #region Variables
        string entityName;
        string entityType;
        string entityId;
        #endregion

        public string Data { get; set; }
        public void SetData<T>(T item) where T:IEntity
        {
            this.entityId = item.EntityId;
            this.entityName = item.EntityName;
            this.entityType = item.EntityType;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            x.Serialize(writer,item);
            Data= sb.ToString();
        }
        public T GetData<T>()
        {
            System.IO.StringReader reader = new System.IO.StringReader(Data);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T item = (T)x.Deserialize(reader);
            return item;
        }

        public virtual string EntityId
        {
            get { return this.entityId; }
        }

        public virtual string EntityName
        {
            get { return this.entityName; }
        }

        public virtual string EntityType
        {
            get { return this.entityType; }
        }
    }
}
