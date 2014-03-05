using System;
using LoggingSystem.Interfaces;
using NLog;

namespace LoggingSystem.LogPublisher
{
    public abstract class BaseLogPublisher : ILogPublisher
    {
        protected Logger Logger;

        protected BaseLogPublisher(Logger logger)
        {
            Logger = logger;
        }

        public abstract bool IsLogLevelEnabled { get; }
        public abstract void LogMessage(string message);
        public abstract void LogException(string message, Exception ex);
    }
}