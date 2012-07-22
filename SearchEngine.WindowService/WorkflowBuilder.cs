using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Workflow;
using RLM.Core.Entity;
using SearchEngine.WindowService.Workflow;

namespace SearchEngine.WindowService
{
    public class WorkflowBuilder
    {
        public static RLM.Core.Framework.Workflow.WorflowActivity<RLM.Core.Entity.IEntity> CreateWorkflow(RLM.Core.Entity.IEntity entity)
        {
            WorflowActivity<IEntity> activity = new WorflowActivity<IEntity>();
            activity.AddChild(new TestActivity());
            ///TO DO
            return activity;
        }
    }
}
