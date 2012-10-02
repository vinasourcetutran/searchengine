using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data
{
    public class BaseDataWriter<EntityType> : IDataWriter<EntityType> where EntityType:IEntity
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public BaseDataWriter(IConfigurable config) { }
        #endregion

        #region Public methods
        public virtual void InsertOrUpdate(IList<EntityType> items)
        {
            throw new NotImplementedException();
        }

        public virtual void InsertOrUpdate(EntityType item)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(EntityType entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        #endregion
        
    }
}
