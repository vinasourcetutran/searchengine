using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Workflow
{
    public interface IWorkflow<T>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Public methods
        WorflowActivity<T> BuildWorkflow(T item);
        #endregion

        #region Private methods
        #endregion
    }
}
