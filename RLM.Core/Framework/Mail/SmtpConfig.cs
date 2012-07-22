using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLM.Core.Framework.Mail
{
    public class SmtpConfig
    {
        #region Variables
        string host;
        int port;
        string username;
        string password;
        #endregion
        #region Properties
        public string Host
        {
            get { return this.host; }
            set { this.host = value; }
        }
        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }
        public string UserName
        {
            get { return this.username; }
            set { this.username = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        #endregion
    }
}
