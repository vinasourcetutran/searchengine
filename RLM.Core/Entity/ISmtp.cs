using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Entity
{
    public interface ISmtp
    {
        string ServerName { get; }
        int Port { get; }
        String UserName { get; }
        string Password { get; }
        bool EnableSSL { get; }
        string DefaultSender { get; }
        string DefaultBCC { get; set; }
        string DefaultCC { get; set; }
    }
}
