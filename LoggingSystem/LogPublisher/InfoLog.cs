using System;
using NLog;

namespace LoggingSystem.LogPublisher
{
    class InfoLog : BaseLogPublisher
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsLogLevelEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public override void LogMessage(string message)
        {
            _logger.Info(message);
        }

        public override void LogException(string message, Exception ex)
        {
            _logger.InfoException(message, ex);
        }
    }
}