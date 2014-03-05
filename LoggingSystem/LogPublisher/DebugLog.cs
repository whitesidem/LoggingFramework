using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class DebugLog : BaseLogPublisher
    {
        public DebugLog(Logger logger) : base(logger)
        {
        }

        public override bool IsLogLevelEnabled
        {
            get { return Logger.IsDebugEnabled; }
        }

        public override void LogMessage(string message)
        {
            Logger.Debug(message);
        }

        public override void LogException(string message, Exception ex)
        {
            Logger.DebugException(message, ex);
        }
    }
}