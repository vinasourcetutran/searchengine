using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data
{
    public interface IDataReader<EntityType> where EntityType : IEntity
    {
        IList<EntityType> GetList(int pagesize, int pageIndex, string orderBy);
        EntityType Get();
        EntityType Get(string item);
    }
}
