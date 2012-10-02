using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RLM.Core.Framework.Log;

namespace RLM.Core.Entity
{
    public class BaseEntity:IEntity
    {

        public virtual string EntityId
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public virtual string EntityName
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string EntityType
        {
            get { return this.GetType().FullName; }
        }
    }
}
