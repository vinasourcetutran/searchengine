using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using RLM.Core.Framework.Log;
using System.Timers;

namespace SearchEngine.WindowService
{
    public partial class Service : ServiceBase
    {
        Timer timer;
        public Service()
        {
            InitializeComponent();
            
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Logger.Info("Timer start ...");
            timer.Stop();
            timer.Enabled = false;
            WindowServiceHelper.Start();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("Start window service ...");
            timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            
        }

        protected override void OnStop()
        {
            Logger.Info("Winservice stopped ...");
            WindowServiceHelper.Stop();
        }
    }
}
