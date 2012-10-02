using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data
{
    public interface IDataWriter<EntityType> where EntityType:IEntity
    {
        void InsertOrUpdate(IList<EntityType> items);
        void InsertOrUpdate(EntityType item);
        bool Delete(EntityType item);
    }
}
