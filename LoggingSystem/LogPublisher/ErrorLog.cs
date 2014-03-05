using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class ErrorLog : BaseLogPublisher
    {
        public ErrorLog(Logger logger) : base(logger)
        {
        }

        public override bool IsLogLevelEnabled
        {
            get { return Logger.IsErrorEnabled; }
        }

        public override void LogMessage(string message)
        {
            Logger.Error(message);
        }

        public override void LogException(string message, Exception ex)
        {
            Logger.ErrorException(message, ex);
        }
    }
}