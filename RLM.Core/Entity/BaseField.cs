using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public class BaseField
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DataTypeEnum DataType { get; set; }
        public EditorTypeEnum Editor { get; set; }
        public int Priority { get; set; }
        #endregion
    }
}
