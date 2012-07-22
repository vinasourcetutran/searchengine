using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public class ThemeCollection:List<Theme>
    {
        public Theme GetByKey(string key)
        {
            foreach (Theme item in this)
            {
                if (item.Key.Equals(key,StringComparison.CurrentCultureIgnoreCase)) { return item; }
            }
            return null;
        }

        public Theme GetByGuid(string guid)
        {
            foreach (Theme item in this)
            {
                if (item.Guid.Equals(guid,StringComparison.CurrentCultureIgnoreCase)) { return item; }
            }
            return null;
        }

        public Theme GetById(int id)
        {
            foreach (Theme item in this)
            {
                if (item.Id == id) { return item; }
            }
            return null;
        }
    }
    public enum ThemeColumn{
        Id,
        Name,
        Key,
        Guid,
        ThumbnailUrl,
        IsActive,
        IgnoreRegexOnCopyFolder,
        FolderUrl,
        Description
    }
}
