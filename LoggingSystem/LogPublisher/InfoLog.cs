using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class InfoLog : BaseLogPublisher
    {
        public InfoLog(Logger logger) : base(logger)
        {
        }

        public override bool IsLogLevelEnabled
        {
            get { return Logger.IsInfoEnabled; }
        }

        public override void LogMessage(string message)
        {
            Logger.Info(message);
        }

        public override void LogException(string message, Exception ex)
        {
            Logger.InfoException(message, ex);
        }
    }
}