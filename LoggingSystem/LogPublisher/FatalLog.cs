using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class FatalLog : BaseLogPublisher
    {
        public FatalLog(Logger logger) : base(logger)
        {
        }

        public override bool IsLogLevelEnabled
        {
            get { return Logger.IsFatalEnabled; }
        }

        public override void LogMessage(string message)
        {
            Logger.Fatal(message);
        }

        public override void LogException(string message, Exception ex)
        {
            Logger.FatalException(message, ex);
        }
    }
}
