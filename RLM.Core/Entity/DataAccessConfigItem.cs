using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RLM.Core.Entity
{
    [Serializable()]
    public class DataAccessConfigItem
    {
        #region Variables
        #endregion

        #region Properties
        public string Key { get; set; }
         [XmlElement(IsNullable = true)]
        public string DataWriter { get; set; }
         [XmlElement(IsNullable = true)]
        public string DataReader { get; set; }

        [XmlAttribute]
        public bool IsReader { get; set; }
        [XmlAttribute()]
        public bool IsWriter { get; set; }
        [XmlAttribute]
        public bool Enable { get; set; }
        [XmlElement("Param",IsNullable=true)]
        public DataAccessConfigItemParam Param { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public methods

        #endregion

        #region Private methods
        #endregion
    }
}
