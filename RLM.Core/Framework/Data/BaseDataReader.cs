using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Data
{
    public class BaseDataReader<EntityType> : IDataReader<EntityType> where EntityType : IEntity
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public BaseDataReader(IConfigurable config) { }
        #endregion

        #region Public methods
        public virtual IList<EntityType> GetList(int pagesize, int pageIndex, string orderBy)
        {
            throw new NotImplementedException();
        }

        public virtual EntityType Get()
        {
            throw new NotImplementedException();
        }

        public virtual EntityType Get(string item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        #endregion
        
    }
}
