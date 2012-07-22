using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Mail
{
    public interface ISmtp
    {
        string ServerName { get; }
        int Port { get; }
        String UserName { get; }
        string Password { get; }
        bool EnableSSL { get; }
        string DefaultSender { get; }
        string DefaultBCC { get; }
        string DefaultCC { get; }
    }
}
