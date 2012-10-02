using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Workflow
{
    public interface IWorkflow<T> where T:IEntity
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Public methods
        WorflowActivity<T> BuildWorkflow();
        #endregion

        #region Private methods
        #endregion
    }
}
