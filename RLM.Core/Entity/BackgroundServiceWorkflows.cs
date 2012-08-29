using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RLM.Core.Entity
{
    [Serializable()]
    [XmlRootAttribute(ElementName = "BackgroundServiceWorkflows", IsNullable = false)]
    public class BackgroundServiceWorkflows : List<BackgroundServiceWorkflow>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
