using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;
using System.Xml;
using RLM.Core.Framework.Log;

namespace RLM.Core.Framework.Utility
{
    public class ThemeHelper
    {
        public static ThemeCollection LoadThemes(string themeFilePath)
        {
            ThemeCollection items = new ThemeCollection();
            try
            {
                System.Xml.XmlDocument document = new XmlDocument();
                document.Load(themeFilePath);
                if (!document.HasChildNodes || document.ChildNodes.Count < 2) { return items; }
                foreach (XmlNode node in document.ChildNodes[1].ChildNodes)
                {
                    try
                    {
                        Theme item = new Theme();
                        if (node.Attributes[ThemeColumn.Id.ToString()] != null)
                        {
                            item.Id = int.Parse(node.Attributes[ThemeColumn.Id.ToString()].Value);
                        }
                        item.Name = node.Attributes[ThemeColumn.Name.ToString()].Value;
                        if (node.Attributes[ThemeColumn.Guid.ToString()] != null)
                        {
                            item.Guid = node.Attributes[ThemeColumn.Guid.ToString()].Value;
                        }
                        item.Key = node.Attributes[ThemeColumn.Key.ToString()].Value;
                        item.ThumbnailUrl = node.Attributes[ThemeColumn.ThumbnailUrl.ToString()].Value;

                        item.IsActive = true;
                        if (node.Attributes[ThemeColumn.IsActive.ToString()] != null)
                        {
                            item.IsActive = bool.Parse(node.Attributes[ThemeColumn.IsActive.ToString()].Value);
                        }
                        item.IgnoreRegexOnCopyFolder = node.Attributes[ThemeColumn.IgnoreRegexOnCopyFolder.ToString()].Value;
                        item.FolderUrl = node.Attributes[ThemeColumn.FolderUrl.ToString()].Value;
                        item.Description = node.Attributes[ThemeColumn.Description.ToString()].Value;
                        items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RLM.Core.Framework.Log.Logger.Error(ex);
            }
            return items;
        }
    }
}
