using System;
using LoggingSystem.Interfaces;
using LoggingSystem.LogPublisher;
using LoggingSystem.Models;
using NLog;

namespace LoggingSystem
{
    public  class LogPublisherFactory : ILogPublisherFactory
    {
        public  BaseLogPublisher CreatePublisher(LoggingLevel level, Logger logger)
        {
            switch (level)
            {
                case LoggingLevel.Debug:
                    return new DebugLog(logger);
                    break;
                case LoggingLevel.Info:
                    return new InfoLog(logger);
                    break;
                case LoggingLevel.Warn:
                    return new WarnLog(logger);
                    break;
                case LoggingLevel.Error:
                    return new ErrorLog(logger);
                    break;
                case LoggingLevel.Fatal:
                    return new FatalLog(logger);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}