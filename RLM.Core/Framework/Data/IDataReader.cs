using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data
{
    public interface IDataReader<EntityType, IdType> where EntityType : BaseEntity
    {
        IList<EntityType> GetList(int pagesize, int pageIndex, string orderBy);
        EntityType Get();
        EntityType Get(IdType id);
    }
}
