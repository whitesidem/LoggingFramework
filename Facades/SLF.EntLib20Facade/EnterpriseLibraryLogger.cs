using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Slf.EntLibFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILogger"/>
  /// interface which outputs logged data using
  /// the <see cref="Logger"/> of the MS Enterprise
  /// Library.
  /// </summary>
  public class EnterpriseLibraryLogger : LoggerBase
  {
    /// <summary>
    /// Creates a named logger.
    /// </summary>
    /// <param name="name">The logger name.</param>
    public EnterpriseLibraryLogger(string name)
      : base(name)
    {      
    }

    /// <summary>
    /// Writes a log entry to the Enterprise Library's
    /// logging block. Output depends on the logging
    /// block's configuration.
    /// </summary>
    /// <param name="item">A <see cref="LogItem"/> which encapsulates
    /// information to be logged.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      if (item == null) throw new ArgumentNullException("item");

      LogEntry entry = ConvertLogItem(item);
      Logger.Write(entry);
    }


    /// <summary>
    /// Creates a <c>LogEntry</c> instance which can be processed
    /// by the Enterprise Library based on a <see cref="LogItem"/>.
    /// </summary>
    /// <param name="item">A <see cref="LogItem"/> which encapsulates information
    /// to be logged.</param>
    /// <returns>An Enterprise Library item which corresponds
    /// to the submitted <c>LogItem</c>.</returns>
    private static LogEntry ConvertLogItem(LogItem item)
    {
      //assign properties
      LogEntry entry = new LogEntry();
      entry.Message = item.Message;
      entry.Title = item.Title;
      entry.EventId = item.EventId ?? 0;
      entry.Severity = GetTraceEventType(item.LogLevel);
      entry.TimeStamp = item.Timestamp.DateTime;

      if (item.Exception != null)
      {
        entry.AddErrorMessage(item.Exception.ToString());
      }

      // Map logger name to EntLib categories
      if (!string.IsNullOrEmpty(item.LoggerName))
      {
        string[] categories = item.LoggerName.Split(',');
        foreach (string category in categories)
        {
          entry.Categories.Add(category.Trim());
        }
      }


      return entry;
    }


    private static TraceEventType GetTraceEventType(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Debug:
          return TraceEventType.Verbose;
        case LogLevel.Undefined:
        case LogLevel.Info:
          return TraceEventType.Information;
        case LogLevel.Warn:
          return TraceEventType.Warning;
        case LogLevel.Error:
          return TraceEventType.Error;
        case LogLevel.Fatal:
          return TraceEventType.Critical;
        default:
          System.Diagnostics.Debug.Fail("Unknown log level received: " + logLevel);
          return TraceEventType.Critical;
      }
    }
  }
}