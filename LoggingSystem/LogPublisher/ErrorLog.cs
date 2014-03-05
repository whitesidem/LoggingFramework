using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class ErrorLog : BaseLogPublisher
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsLogLevelEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public override void LogMessage(string message)
        {
            _logger.Error(message);
        }

        public override void LogException(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }
    }
}