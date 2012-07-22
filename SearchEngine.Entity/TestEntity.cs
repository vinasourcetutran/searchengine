using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.Bot.Entity
{
    public class TestEntity:IEntity
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Public methods
        public string Name { get; set; }
        #endregion

        #region Private methods
        #endregion
        public string EntityId
        {
            get { throw new NotImplementedException(); }
        }

        public string EntityName
        {
            get { return this.Name; }
        }

        public string EntityType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
