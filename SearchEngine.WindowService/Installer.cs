using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace SearchEngine.WindowService
{
    [RunInstaller(true)]
    public partial class SearchEngineWindoserviceInstaller : System.Configuration.Install.Installer
    {
        public SearchEngineWindoserviceInstaller()
        {
            InitializeComponent();
            ServiceProcessInstaller spi = new ServiceProcessInstaller();
            spi.Account = ServiceAccount.LocalSystem;
            //
            ServiceInstaller si = new ServiceInstaller();
            si.StartType = ServiceStartMode.Manual;

            si.ServiceName = "SearchEngine Winservice";

            Installers.AddRange(new Installer[] { spi, si });
        }
    }
}
