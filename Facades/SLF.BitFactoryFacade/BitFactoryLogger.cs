using System;
using System.Diagnostics;
using BitFactory.Logging;

namespace Slf.BitFactoryFacade
{
  /// <summary>
  /// Decorates and forwards log messages to an aribtrary
  /// <see cref="BitFactory.Logging.Logger"/>.
  /// </summary>
  public class BitFactoryLogger : FormattableLoggerBase
  {
    /// <summary>
    /// The logger that receives the entries to be logged.
    /// </summary>
    public Logger Logger { get; private set; }


    /// <summary>
    /// Logs data through a given <paramref name="logger"/>.
    /// </summary>
    /// <param name="logger">An arbitrary logger implementation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logger"/>
    /// is a null reference.</exception>
    public BitFactoryLogger(Logger logger)
    {
      if (logger == null) throw new ArgumentNullException("logger");
      Logger = logger;
    }


    /// <summary>
    /// Creates a new log entry based on a given log item.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      if (item == null) throw new ArgumentNullException("item");

      LogSeverity severity = Extensions.GetLogSeverity(item.LogLevel);
      Logger.Log(severity, item.LoggerName, FormatItem(item));  
    }



    /// <summary>
    /// Creates a simple logger that logs data to a single file.
    /// </summary>
    /// <param name="fileName">The log file to be written to.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="fileName"/>
    /// is a null reference.</exception>
    public static ILogger CreateSingleFileLogger(string fileName)
    {
      if (fileName == null) throw new ArgumentNullException("fileName");

      var logger = new FileLogger(fileName);
      return new BitFactoryLogger(logger);
    }



    /// <summary>
    /// Creates a rolling file logger that switches log files depending on the
    /// log's size.
    /// </summary>
    /// <param name="baseFileName">The full path of the base log file,
    /// e.g. <c>C:\Folder\LogFile.txt</c>"</param>
    /// <param name="maxFileSize">The maximum file size.</param>
    /// <returns>Logger instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="baseFileName"/>
    /// is a null reference.</exception>
    public static ILogger CreateRollingFileLogger(string baseFileName, long maxFileSize)
    {
      if (baseFileName == null) throw new ArgumentNullException("baseFileName");

      string fileName = Extensions.GetRollingFileFormat(baseFileName);
      
      var strategy = new RollingFileLogger.RollOverSizeStrategy(fileName, maxFileSize);
      var logger = new RollingFileLogger(strategy);
      return new BitFactoryLogger(logger);
    }



    /// <summary>
    /// Creates a rolling file logger that creates log files based on
    /// the logging timestamp.
    /// </summary>
    /// <param name="baseFileName">The full path of the base log file,
    /// e.g. <c>C:\Folder\LogFile.txt</c>"</param>
    /// <param name="dateFormat">A format string that will be used to format the date
    /// portion of the log file name (e.g. <c>yyyyMMdd</c>)</param>
    /// <returns>Logger instance.</returns>
    public static ILogger CreateRollingFileLogger(string baseFileName, string dateFormat)
    {
      string fileName = Extensions.GetRollingFileFormat(baseFileName);

      var strategy = new RollingFileLogger.RollOverDateStrategy(fileName, dateFormat);
      var logger = new RollingFileLogger(strategy);
      return new BitFactoryLogger(logger);
    }


    /// <summary>
    /// Creates a logger that writes to the system event log.
    /// </summary>
    /// <param name="log">The system log that receives logged
    /// messages.</param>
    /// <returns>Logger instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="log"/>
    /// is a null reference.</exception>
    public static ILogger CreateSysEventLogger(EventLog log)
    {
      if (log == null) throw new ArgumentNullException("log");

      var logger = new EventLogLogger(log);
      return new BitFactoryLogger(logger);
    }



  }

}
