using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class WarnLog : BaseLogPublisher
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsLogLevelEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public override void LogMessage(string message)
        {
            _logger.Warn(message);
        }

        public override void LogException(string message, Exception ex)
        {
            _logger.WarnException(message, ex);
        }
    }
}