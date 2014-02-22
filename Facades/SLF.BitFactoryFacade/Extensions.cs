using System.IO;
using BitFactory.Logging;

namespace Slf.BitFactoryFacade
{
  /// <summary>
  /// Simple extension methods.
  /// </summary>
  internal static class Extensions
  {
    public static LogSeverity GetLogSeverity(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Info:
          return LogSeverity.Info;
        case LogLevel.Error:
          return LogSeverity.Error;
        case LogLevel.Fatal:
          return LogSeverity.Critical;
        case LogLevel.Warn:
          return LogSeverity.Warning;
        case LogLevel.Debug:
          return LogSeverity.Debug;
        default:
          return LogSeverity.Info;
      }
    }


    public static string GetRollingFileFormat(string baseFileName)
    {
      if (Path.HasExtension(baseFileName))
      {
        int extensionIndex = baseFileName.IndexOf(Path.GetExtension(baseFileName));
        return baseFileName.Insert(extensionIndex, "{0}");
      }

      return baseFileName + "{0}";
    }
  }
}