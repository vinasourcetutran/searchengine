using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public interface IEntity
    {
        string EntityId
        {
            get;
        }
        string EntityName
        {
            get;
        }
        string EntityType
        {
            get;
        }
    }
}
