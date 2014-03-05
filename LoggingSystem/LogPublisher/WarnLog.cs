using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class WarnLog : BaseLogPublisher
    {
        public WarnLog(Logger logger) : base(logger)
        {
        }

        public override bool IsLogLevelEnabled
        {
            get { return Logger.IsWarnEnabled; }
        }

        public override void LogMessage(string message)
        {
            Logger.Warn(message);
        }

        public override void LogException(string message, Exception ex)
        {
            Logger.WarnException(message, ex);
        }
    }
}