using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Data
{
    public interface IDataWriter<EntityType, IdType>
    {
        void InsertOrUpdate(IList<EntityType> items);
        void InsertOrUpdate(EntityType item);
        bool Delete(IdType id);
    }
}
