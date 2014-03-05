using System;

namespace LoggingSystem.Interfaces
{
    internal interface ILogPublisher
    {
        bool IsLogLevelEnabled { get; }
        void LogMessage(string message);
        void LogException(string message, Exception ex);
    }
}