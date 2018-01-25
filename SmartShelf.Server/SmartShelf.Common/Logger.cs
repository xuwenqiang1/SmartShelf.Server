using System;
using log4net;

namespace SmartShelf.Common
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Logger _logger;
        private static readonly object LockObject = new object();
        private Logger() { }
        public static Logger GetLoggerInstance()
        {
            lock (LockObject)
            {
                return _logger ?? (_logger = new Logger());
            }
        }
        public void Debug(object message, Exception e = null)
        {
            if (e == null)
            {
                Log.Debug(message);
            }
            else
            {
                Log.Debug(message, e);
            }
        }

        public void Info(object message, Exception e = null)
        {
            if (e == null)
            {
                Log.Info(message);
            }
            else
            {
                Log.Info(message, e);
            }
        }

        public void Error(object message, Exception e = null)
        {
            if (e == null)
            {
                Log.Error(message);
            }
            else
            {
                Log.Error(message, e);
            }
        }

        public void DebugFormat(string message, params object[] args)
        {
            Log.DebugFormat(message, args);
        }

        public void InfoFormat(string message, params object[] args)
        {
            Log.InfoFormat(message, args);
        }

        public void ErrorFormat(string message, params object[] args)
        {
            Log.ErrorFormat(message, args);
        }
    }
}