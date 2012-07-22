using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Framework.Mail;

namespace RLM.Core.Framework.Utility
{
    public class TemplateHelper
    {
        public static void Replace(string file, Entity.Hash<string> fields)
        {
            Replace(file, file, fields);
        }

        public static void Replace(string templateFile, string newFileName, Entity.Hash<string> fields)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string key in fields.Keys)
            {
                parameters.Add(key, fields[key]);
            }
            string newContent = TemplateReader.GetXmlContent("template","{{0}}",templateFile, parameters);
            IOHelper.WriteToFile(newFileName, newContent, false);
        }
    }
}
