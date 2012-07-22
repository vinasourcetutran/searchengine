using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.Reflection;

namespace RLM.Core.Framework.Log
{
    public class Logger
    {
        // Fields
        private const string FRAMEWORK_LOGGER = "FrameworkLogger";
        private static readonly ILog frameworkLogger;

        // Methods
        static Logger()
        {
            XmlConfigurator.Configure();
            frameworkLogger = LogManager.GetLogger("FrameworkLogger");
        }

        public static void Debug(object message)
        {
            frameworkLogger.Debug(message);
        }

        public static void Debug(object message, Exception ex)
        {
            frameworkLogger.Debug(message, ex);
        }

        public static void Debug(string format, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                frameworkLogger.Debug(string.Format(format, args));
            }
            else
            {
                frameworkLogger.Debug(format);
            }
        }

        public static void Error(object message)
        {
            frameworkLogger.Error(message);
        }

        public static void Error(object message, Exception ex)
        {
            frameworkLogger.Error(message, ex);
        }

        public static void Error(string format, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                frameworkLogger.Error(string.Format(format, args));
            }
            else
            {
                frameworkLogger.Error(format);
            }
        }

        public static void Fatal(object message)
        {
            frameworkLogger.Fatal(message);
        }

        public static void Fatal(object message, Exception ex)
        {
            frameworkLogger.Fatal(message, ex);
        }

        public static void Fatal(string format, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                frameworkLogger.Fatal(string.Format(format, args));
            }
            else
            {
                frameworkLogger.Fatal(format);
            }
        }

        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static ILog GetLogger(Assembly assembly, string name)
        {
            return LogManager.GetLogger(assembly, name);
        }

        public static ILog GetLogger(Assembly assembly, Type type)
        {
            return LogManager.GetLogger(assembly, type);
        }

        public static ILog GetLogger(string repository, string name)
        {
            return LogManager.GetLogger(repository, name);
        }

        public static ILog GetLogger(string repository, Type type)
        {
            return LogManager.GetLogger(repository, type);
        }

        public static void Info(object message)
        {
            frameworkLogger.Info(message);
        }

        public static void Info(object message, Exception ex)
        {
            frameworkLogger.Info(message, ex);
        }

        public static void InfoWithParam(string format, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                frameworkLogger.Info(string.Format(format, args));
            }
            else
            {
                frameworkLogger.Info(format);
            }
        }

        public static void Info(string format, params object[] args)
        {
            InfoWithParam(format, args);
        }

        public static void Warn(object message)
        {
            frameworkLogger.Warn(message);
        }

        public static void Warn(object message, Exception ex)
        {
            frameworkLogger.Warn(message, ex);
        }

        public static void Warn(string format, params object[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                frameworkLogger.Warn(string.Format(format, args));
            }
            else
            {
                frameworkLogger.Warn(format);
            }
        }

        // Properties
        public static bool IsDebugEnabled
        {
            get
            {
                return frameworkLogger.IsDebugEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return frameworkLogger.IsErrorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return frameworkLogger.IsFatalEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return frameworkLogger.IsInfoEnabled;
            }
        }

        public static bool IsWarnEnabled
        {
            get
            {
                return frameworkLogger.IsWarnEnabled;
            }
        }
    }


}
