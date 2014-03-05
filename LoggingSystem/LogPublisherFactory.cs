using System;
using LoggingSystem.Interfaces;
using LoggingSystem.LogPublisher;
using LoggingSystem.Models;
using NLog;

namespace LoggingSystem
{
    public  class LogPublisherFactory : ILogPublisherFactory
    {
        public  BaseLogPublisher CreatePublisher(LoggingLevel level)
        {
            switch (level)
            {
                case LoggingLevel.Debug:
                    return new DebugLog();
                    break;
                case LoggingLevel.Info:
                    return new InfoLog();
                    break;
                case LoggingLevel.Warn:
                    return new WarnLog();
                    break;
                case LoggingLevel.Error:
                    return new ErrorLog();
                    break;
                case LoggingLevel.Fatal:
                    return new FatalLog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}