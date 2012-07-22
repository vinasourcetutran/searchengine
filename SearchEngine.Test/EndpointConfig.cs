using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace SearchEngine.Test
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client
    {
    }
    internal class SetupDataBus : IWantCustomInitialization
    {
        public static string BasePath = "..\\..\\..\\storage";

        public void Init()
        {
            Configure.Instance
                .FileShareDataBus(BasePath)
                .UnicastBus().DoNotAutoSubscribe();//until ICommand is introduced
        }
    }
}
