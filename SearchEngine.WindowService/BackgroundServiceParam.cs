using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using RLM.Core.Framework.Workflow;

namespace SearchEngine.WindowService
{
    public class BackgroundServiceParam
    {
        #region Variables
        #endregion

        #region Properties
        public int Index { get; set; }
        public IWorkflow<BaseEntityObject> Workflow { get; set; }
        public RLM.Core.Framework.Data.IDataReader<BaseEntityObject> DataReader { get; set; }
        public RLM.Core.Framework.Data.IDataWriter<BaseEntityObject> DataWriter { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
