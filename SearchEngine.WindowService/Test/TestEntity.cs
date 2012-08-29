using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.WindowService.Test
{
    public class TestEntity:IEntity
    {
        #region Variables
        #endregion

        #region Properties
        public string Name { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
        public string EntityId
        {
            get { throw new NotImplementedException(); }
        }

        public string EntityName
        {
            get { return Name; }
        }

        public string EntityType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
