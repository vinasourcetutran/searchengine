﻿using System;
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
        public static RLM.Core.Framework.Workflow.WorflowActivity<RLM.Core.Entity.BaseEntityObject> CreateWorkflow()
        {
            WorflowActivity<BaseEntityObject> activity = new WorflowActivity<BaseEntityObject>();
            activity.AddChild(new TestActivity());
            ///TO DO
            return activity;
        }
    }
}
