using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity
{
    public class Country:BaseObject
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsoCode { get; set; }
        public Language DefaultLanguage { get; set; }
        public bool Enable { get; set; }
        public int Priority { get; set; }
    }
}
