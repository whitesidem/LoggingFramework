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
using log4net.Config;
using Slf;
using log4net;

namespace SLF.Log4netFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILoggerFactory"/>
  /// interface which creates <see cref="ILogger"/> instances
  /// that use the log4net framework as the underlying logging
  /// mechanism.
  /// </summary>
  public class Log4netLoggerFactory : ILoggerFactory
  {
    static Log4netLoggerFactory()
    {
      // load the log4net configuration from the application configuration.
      XmlConfigurator.Configure();
    }

    public ILogger GetLogger(string name)
    {
      return new Log4netLogger(LogManager.GetLogger(name));
    }
  }
}
