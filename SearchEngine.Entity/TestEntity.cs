using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.Bot.Entity
{
    [Serializable]
    public class TestEntity:BaseEntity
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

        public override string EntityName
        {
            get { return this.Name; }
        }

        
        
    }
}
