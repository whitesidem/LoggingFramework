using LoggingSystem.LogPublisher;
using LoggingSystem.Models;
using NLog;

namespace LoggingSystem.Interfaces
{
    public interface ILogPublisherFactory
    {
        BaseLogPublisher CreatePublisher(LoggingLevel level, Logger logger);
    }
}