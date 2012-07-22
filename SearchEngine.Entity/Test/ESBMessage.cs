using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using RLM.Core.Entity;

namespace SearchEngine.Bot.Entity.Test
{
    public class ESBMessage : IMessage, IEntity
    {
        public string Name { get; set; }

        public string EntityId
        {
            get { throw new NotImplementedException(); }
        }

        public string EntityName
        {
            get { return Name; }
        }

        public string EntityType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
