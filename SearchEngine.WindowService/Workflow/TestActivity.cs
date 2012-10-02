using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Log;
using SearchEngine.Bot.Entity;
using RLM.Core.Entity;

namespace SearchEngine.WindowService.Workflow
{
    public class TestActivity : RLM.Core.Framework.Workflow.WorflowActivity<BaseEntityObject>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Public methods
        public override bool IsValid()
        {
            return true;
        }
        protected override void Excute()
        {
            TestEntity entity = Item.GetData<TestEntity>();
            Logger.Info("item {0} was processed with id {1}.",entity.EntityName, entity.EntityId);
        }
        #endregion

        #region Private methods
        #endregion
    }
}
