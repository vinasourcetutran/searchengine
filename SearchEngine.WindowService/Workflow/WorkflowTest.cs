using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using RLM.Core.Framework.Workflow;

namespace SearchEngine.WindowService.Workflow
{
    public class WorkflowTest : IWorkflow<IEntity>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public WorkflowTest() { }
        #endregion

        #region Public methods
        public WorflowActivity<IEntity> BuildWorkflow(IEntity item)
        {
            WorflowActivity<IEntity> activity = new WorflowActivity<IEntity>();
            activity.AddChild(new TestActivity());
            ///TO DO
            return activity;
        }
        #endregion

        #region Private methods
        #endregion
        
    }
}
