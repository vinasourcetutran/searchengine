﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RLM.Core.Entity
{
    [Serializable]
    [XmlRootAttribute(ElementName = "DataAccessConfigs", IsNullable = false)]
    public class DataAccessConfigs : List<DataAccessConfigItem>
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
