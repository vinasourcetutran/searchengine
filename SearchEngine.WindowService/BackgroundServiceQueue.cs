using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.WindowService
{
    public class BackgroundServiceQueue
    {
        #region Variables
        #endregion

        #region Constructor
        static BackgroundServiceQueue()
        {
            Entity = new QueueIEntity();
        }
        #endregion

        #region Properties
        public static QueueIEntity Entity { get; set; }
        #endregion

        #region Public methods
        #endregion 

        #region Private methods
        #endregion
    }
}
