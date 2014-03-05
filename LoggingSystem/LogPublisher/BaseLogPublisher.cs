using System;
using LoggingSystem.Interfaces;

namespace LoggingSystem.LogPublisher
{
    public abstract class BaseLogPublisher : ILogPublisher
    {

        public abstract bool IsLogLevelEnabled { get; }
        public abstract void LogMessage(string message);
        public abstract void LogException(string message, Exception ex);
    }
}