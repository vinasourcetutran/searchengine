using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;

namespace SearchEngine.WindowService.Workflow
{
    public class TestActivity : RLM.Core.Framework.Workflow.WorflowActivity<RLM.Core.Entity.IEntity>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Public methods
        public override bool IsValid()
        {
            return true;
        }
        protected override void Excute()
        {
            Logger.Info("item {0} was processed.",this.Item.EntityName);
        }
        #endregion

        #region Private methods
        #endregion
    }
}
