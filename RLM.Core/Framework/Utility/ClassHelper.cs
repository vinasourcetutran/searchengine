using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Utility
{
    public class ClassHelper
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Public methods
        public static T CreateInstance<T>(string className)
        {
            return (T)Activator.CreateInstance(Type.GetType(className));
        }
        #endregion

        #region Private methods
        #endregion
    }
}
