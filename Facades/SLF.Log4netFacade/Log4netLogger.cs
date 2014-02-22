// SLF.NET - Simple Logging Façade for .NET
// Contact and Information: http://slf.codeplex.com
//
// This library is free software; you can redistribute it and/or
// modify it in any way you see fit as long as this copyright
// notice is not being removed.
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE

using System;
using Slf;
using log4net;
using System.Text;
using Slf.Formatters;

namespace SLF.Log4netFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILogger"/>
  /// interface which logs messages via the log4net
  /// framework.
  /// </summary>
  public class Log4netLogger : FormattableLoggerBase
  {
    /// <summary>
    /// The log4net logger which this class wraps.
    /// </summary>
    private readonly ILog logger;

    /// <summary>
    /// Constructs an instance of <see cref="Log4netLogger"/>
    /// by wrapping a log4net logger
    /// </summary>
    /// <param name="logger">The log4net logger to wrap</param>
    internal Log4netLogger(ILog logger) : base(SingleLineFormatter.Instance)
    {
      this.logger = logger;
    }

    /// <summary>
    /// Logs the given message. Output depends on the associated
    /// log4net configuration.
    /// </summary>
    /// <param name="item">A <see cref="LogItem"/> which encapsulates
    /// information to be logged.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      if (item == null) throw new ArgumentNullException("item");

      string message = FormatItem(item);

      switch (item.LogLevel)
      {
        case LogLevel.Fatal:
          logger.Fatal(message, item.Exception);
          break;

        case LogLevel.Error:
          logger.Error(message, item.Exception);
          break;

        case LogLevel.Warn:
          logger.Warn(message, item.Exception);
          break;

        case LogLevel.Info:
          logger.Info(message, item.Exception);
          break;

        case LogLevel.Debug:
          logger.Debug(message, item.Exception);
          break;

        default:
          logger.Info(message, item.Exception);
          break;        
      }
    }

    /// <summary>
    /// Overriden to delegate to the log4net IsXxxEnabled
    /// properties.
    /// </summary>
    protected override bool IsLogLevelEnabled(Slf.LogLevel level)
    {
      switch (level)
      {
        case Slf.LogLevel.Debug:
          return logger.IsDebugEnabled;
        case Slf.LogLevel.Error:
          return logger.IsErrorEnabled;
        case Slf.LogLevel.Fatal:
          return logger.IsFatalEnabled;
        case Slf.LogLevel.Info:
          return logger.IsInfoEnabled;
        case Slf.LogLevel.Warn:
          return logger.IsWarnEnabled;
        default:
          return true;
      }
    }

  }
}
