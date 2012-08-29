using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RLM.Core.Entity
{
    public class DataAccessConfigItemParam
    {
        #region Variables
        #endregion

        #region Properties
        [XmlAttribute]
        public string InstanceClassName { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
