using System;
using System.Reflection;
using log4net;

namespace AutoBuild.Core
{
    public class ConsoleLogger : IConsole
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ILog Logger
        {
            set { logger = value; }
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }
    }
}