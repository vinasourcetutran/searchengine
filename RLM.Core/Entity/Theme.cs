using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public class Theme
    {
        #region Prperties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Guid { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsActive { get; set; }
        public string IgnoreRegexOnCopyFolder { get; set; }
        public string FolderUrl { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
