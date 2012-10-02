using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data;
using RLM.Core.Framework.Data.MessageQueue;
using RLM.Core.Entity;

namespace SearchEngine.WindowService.Workflow
{
    public class ConsoleReader : MessageQueueReader<BaseEntityObject>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public ConsoleReader(IConfigurable config):base(config){
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        #endregion
    }
}
