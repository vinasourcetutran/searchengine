using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Data.MessageQueue;
using RLM.Core.Entity;
using RLM.Core.Framework.Data;

namespace SearchEngine.WindowService.Workflow
{
    public class ConsoleWriter : BaseDataWriter<IEntity>
    {
        #region Variables
        #endregion

        #region Properties

        #endregion

        #region Constructor
        ConsoleWriter(IConfigurable config) : base(config) { }
        #endregion

        #region Public methods
        public override void InsertOrUpdate(IEntity item)
        {
            Console.WriteLine(item.EntityName);
        }
        #endregion

        #region Private methods
        #endregion
    }
}
