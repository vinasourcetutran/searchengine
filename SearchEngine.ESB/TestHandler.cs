using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEngine.Bot.Entity;
using SearchEngine.Bot.Entity.Test;

namespace SearchEngine.ESB
{
    public class TestHandler : BaseESBHandler<ESBMessage>
    {
        public override void Handle(ESBMessage message)
        {
            Console.WriteLine("name of entity is:"+ message.Name);
        }
    }
}
