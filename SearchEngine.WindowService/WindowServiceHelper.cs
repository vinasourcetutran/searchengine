using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using RLM.Core.Framework.Log;
using RLM.Core.Entity;
using RLM.Core.Framework.Workflow;

namespace SearchEngine.WindowService
{
    public class WindowServiceHelper
    {
        #region Properties
        public static bool IsRunning { get; set; }
        #endregion

        static void DoWork(object arg)
        {
            try
            {
                Logger.Info("Start thread #" + arg);
                while (WindowServiceHelper.IsRunning)
                {
                    IEntity entity = BackgroundServiceQueue.Entity.Dequeue();
                    if (entity == null)
                    {
                        Logger.Info("Thread #{0} waits for data ...", arg);
                        Thread.Sleep(1500);
                    }
                    WorflowActivity<IEntity> activity = WorkflowBuilder.CreateWorkflow(entity);
                    activity.Excute(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public static void Start()
        {
            try
            {
                WindowServiceHelper.IsRunning = true;

                int maxThread = SearchEngine.Configuration.Configuration.GetInstance().BackgroundService.MaxThread;
                for (var index = 0; index < maxThread; index++)
                {
                    Thread thr = new Thread(DoWork);
                    thr.Start(index);
                }
                while (WindowServiceHelper.IsRunning)
                {
                    Logger.Info("Waiting ...");
                    Thread.Sleep(SearchEngine.Configuration.Configuration.GetInstance().BackgroundService.MainThreadWaitingTime);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static void Stop()
        {
            Logger.Info("Winservice begins stopping ...");
            WindowServiceHelper.IsRunning = false;
        }
    }
}
