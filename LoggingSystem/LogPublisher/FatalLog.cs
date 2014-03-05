using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class FatalLog : BaseLogPublisher
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsLogLevelEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        public override void LogMessage(string message)
        {
            _logger.Fatal(message);
        }

        public override void LogException(string message, Exception ex)
        {
            _logger.FatalException(message, ex);
        }
    }
}
