using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using SearchEngine.Entity.Html;
using RLM.Core.Framework.Log;

namespace SearchEngine.WindowService.Workflow.Url
{
    public class UrlCrawlerActivity : RLM.Core.Framework.Workflow.WorflowActivity<BaseEntityObject>
    {
        public override bool IsValid()
        {
            return true;
        }

        protected override void Excute()
        {
            HtmlUrl url = this.Item as HtmlUrl;
            Logger.Info("Process item:Id-{0};Name-{1};Type-{2}", url.EntityId, url.EntityName, url.EntityType);
        }
    }
}
