using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity
{
    public class Language: BaseObject
    {
        public int LanguageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public int Priority { get; set; }
    }
}
