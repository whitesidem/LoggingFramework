using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class DebugLog : BaseLogPublisher
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsLogLevelEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public override void LogMessage(string message)
        {
            _logger.Debug(message);
        }

        public override void LogException(string message, Exception ex)
        {
            _logger.DebugException(message, ex);
        }
    }
}