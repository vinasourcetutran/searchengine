﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using RLM.Core.Framework.Log;
using RLM.Core.Entity;
using RLM.Core.Framework.Workflow;
using RLM.Core.Framework.Utility;
using System.Data;

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
                BackgroundServiceParam param = (BackgroundServiceParam)arg;
                Logger.Info("Start thread #" + param.Index);
                while (WindowServiceHelper.IsRunning)
                {
                    BaseEntityObject entity = param.DataReader.Get();// BackgroundServiceQueue.Entity.Dequeue();
                    if (entity == null)
                    {
                        Logger.Info("Thread #{0} ({1}/{2}) waits for data ...", param.Index, param.Workflow.GetType().FullName, param.DataReader.GetType().FullName);
                        Thread.Sleep(5000);
                        continue;
                    }
                    WorflowActivity<BaseEntityObject> activity = param.Workflow.BuildWorkflow();// WorkflowBuilder.CreateWorkflow(entity);
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
                Thread.Sleep(15000);
                BackgroundServiceQueue.InitDataReaderAndWriter();
                WindowServiceHelper.IsRunning = true;
                BackgroundServiceWorkflows workflows = XmlHelper.Deserialize<BackgroundServiceWorkflows>(Configuration.Configuration.GetInstance().BackgroundService.WorkflowConfigFile);
                foreach (BackgroundServiceWorkflow item in workflows)
                {
                    if (!item.Enable) { continue; }
                    RLM.Core.Framework.Data.IDataReader<BaseEntityObject> reader = null;
                    if (BackgroundServiceQueue.DataReader.ContainsKey(item.Key))
                    {
                        reader = BackgroundServiceQueue.DataReader[item.Key];
                    }

                    RLM.Core.Framework.Data.IDataWriter<BaseEntityObject> writer = null;
                    if (BackgroundServiceQueue.DataWriter.ContainsKey(item.Key))
                    {
                        reader = BackgroundServiceQueue.DataReader[item.Key];
                    }

                    IWorkflow<BaseEntityObject> workflow = ClassHelper.CreateInstance<IWorkflow<BaseEntityObject>>(item.Workflow);
                    int maxThread = item.MaxThread;
                    for (var index = 0; index < maxThread; index++)
                    {
                        Thread thr = new Thread(DoWork);
                        thr.Start(new BackgroundServiceParam() { Index = index, DataReader = reader, DataWriter = writer, Workflow = workflow });
                    }
                }
                //int maxThread = SearchEngine.Configuration.Configuration.GetInstance().BackgroundService.MaxThread;
                //for (var index = 0; index < maxThread; index++)
                //{
                //    Thread thr = new Thread(DoWork);
                //    thr.Start(index);
                //}
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
