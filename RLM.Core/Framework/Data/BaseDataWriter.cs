using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Data
{
    public class BaseDataWriter<EntityType, IdType> : IDataWriter<EntityType, IdType>
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

        public virtual bool Delete(IdType id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        #endregion
        
    }
}
