using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data.MessageQueue;
using RLM.Core.Framework.Data;

namespace SearchEngine.Test.ConsoleApp
{
    public class TestDataReader : MessageQueueReader<SearchEngine.Bot.Entity.TestEntity>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #region Constructor
        public TestDataReader(IConfigurable config)
            : base(config)
        {
        }
        #endregion
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
