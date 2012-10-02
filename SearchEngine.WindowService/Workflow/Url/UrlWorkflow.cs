using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Workflow;
using RLM.Core.Entity;

namespace SearchEngine.WindowService.Workflow.Url
{
    public class UrlWorkflow : IWorkflow<BaseEntityObject>
    {

        public WorflowActivity<BaseEntityObject> BuildWorkflow()
        {
            WorflowActivity<BaseEntityObject> activity = new WorflowActivity<BaseEntityObject>();
            activity.AddChild(new UrlCrawlerActivity());
            ///TO DO
            return activity;
        }
    }
}
