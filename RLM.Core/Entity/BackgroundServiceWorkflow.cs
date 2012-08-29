using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RLM.Core.Entity
{
    [Serializable()]
    public class BackgroundServiceWorkflow
    {
        #region Variables
        #endregion

        #region Properties
        public string EntityClassName { get; set; }
        public string Workflow { get; set; }
        [XmlAttribute]
        public int MaxThread { get; set; }
         [XmlAttribute]
        public bool Enable { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
