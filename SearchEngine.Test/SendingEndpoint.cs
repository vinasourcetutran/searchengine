using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Test
{
    using System;
    using NServiceBus;
    using NServiceBus.DataBus;
    using SearchEngine.Bot.Entity.Test;

    public class SendingEndpoint : IWantToRunAtStartup
    {
        readonly IBus bus;

        public SendingEndpoint(IBus bus)
        {
            this.bus = bus;
        }

        public void Run()
        {
            Console.WriteLine("Before send");
            bus.Send<ESBMessage>(s => { s.Name = "Test"; });
            Console.WriteLine("After send");
        }


        public void Stop()
        {
        }

    }
}
